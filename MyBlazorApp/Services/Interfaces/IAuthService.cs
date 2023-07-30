using MyDtos;

namespace MyBlazorApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> IsAuthenticated();
        Task<bool> Login(LoginDto loginDto);
        Task Logout();
    }
}
