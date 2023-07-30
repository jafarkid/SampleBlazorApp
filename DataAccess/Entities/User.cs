using System.Data;

namespace MyDataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        //public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // One-to-one relationship with Role
        //public int RoleId { get; set; }
        //public Role Role { get; set; }
    }
}