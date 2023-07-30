using MyDataAccess.Entities;
using MyDtos;

namespace MyApiApp.Services
{
    public interface ITokenService
    {
        string CreateToken(UserDto user);
    }
}
