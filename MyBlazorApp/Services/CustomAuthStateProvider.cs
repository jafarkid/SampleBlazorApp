using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace MyBlazorApp.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                string token = await _localStorage.GetItemAsStringAsync("token");

                // Check if the token is null or empty
                if (string.IsNullOrEmpty(token))
                {
                    // If token is null or empty, return unauthenticated state
                    var _user = new ClaimsPrincipal(new ClaimsIdentity());
                    var _state = new AuthenticationState(_user);

                    NotifyAuthenticationStateChanged(Task.FromResult(_state));

                    return _state;
                }

                var Identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

                var user = new ClaimsPrincipal(Identity);
                var state = new AuthenticationState(user);

                NotifyAuthenticationStateChanged(Task.FromResult(state));

                return state;
            }
            catch (InvalidOperationException)
            {
                // If an exception occurs (e.g., token not found or invalid token), return unauthenticated state
                var user = new ClaimsPrincipal(new ClaimsIdentity());
                var state = new AuthenticationState(user);

                NotifyAuthenticationStateChanged(Task.FromResult(state));

                return state;

            }
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
