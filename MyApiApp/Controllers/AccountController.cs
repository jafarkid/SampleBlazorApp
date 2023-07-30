using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiApp.Services;
using MyDataAccess.Entities;
using MyDtos;
using MyRepository.Implementations;
using MyRepository.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MyApiApp.Controllers
{
    [Route("api/Account")]
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _account;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountRepository account, ITokenService tokenService)
        {
            _account = account;
            _tokenService = tokenService;
        }

        [HttpPost("register")] //POST: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _account.UserExists(registerDto.Username))
                return BadRequest("Username is taken");

            var user = await _account.Register(registerDto);
            user.Token = _tokenService.CreateToken(user);

            return user;
        }

        [HttpPost("login")] //POST: api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _account.GetUserByUsernameAsync(loginDto.Username);

            if (user == null)
                return Unauthorized("Invalid Username");

            var isPasswordValid = _account.VerifyPassword(user, loginDto.Password);

            if (!isPasswordValid)
                return Unauthorized("Invalid password");

            var userDto = new UserDto { Username = user.Username };
            userDto.Token = _tokenService.CreateToken(userDto);

            return userDto;
        }

    }
}
