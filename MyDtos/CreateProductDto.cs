using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MyDtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEditing { get; set; } = false;
    }
    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}