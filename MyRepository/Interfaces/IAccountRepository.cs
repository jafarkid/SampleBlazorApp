using MyDataAccess.Entities;
using MyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepository.Interfaces
{
    public interface IAccountRepository
    {
        Task<UserDto> Register(RegisterDto registerDto);
        Task<bool> UserExists(string username);
        Task<User?> GetUserByUsernameAsync(string username);
        bool VerifyPassword(User user, string password);

    }
}
