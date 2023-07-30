using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MyBlazorApp.Components;
using MyBlazorApp.Services.Interfaces;
using MyDtos;

namespace MyBlazorApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly SessionStateService _sessionStateService;


        public AuthService(ILocalStorageService localStorage, HttpClient httpClient, SessionStateService sessionStateService)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _sessionStateService = sessionStateService;
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await _localStorage.GetItemAsync<string>("token");            
            return !string.IsNullOrEmpty(token);
        }

        private async Task<string> GetToken()
        {
            return await _localStorage.GetItemAsync<string>("token");
        }


        public async Task<bool> Login(LoginDto loginDto)
        {
            // Perform login logic and retrieve token
            var response = await this._httpClient.PostAsJsonAsync("api/account/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserDto>();
                // Perform further actions with the user object                

                // Save token to LocalStorage
                await _localStorage.SetItemAsync("token", user.Token);
                await _localStorage.SetItemAsync("userName", user.Username);

                _sessionStateService.Token = user.Token;
            }
            else
            {
                // Handle error cases when login is not successful
            }
            
            return true; // Return true if login is successful
        }

        public async Task Logout()
        {
            // Perform logout logic
            // Clear token from LocalStorage
            await _localStorage.RemoveItemAsync("token");
        }
    }
}