using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.BLL.DTOs.Auth
{
    public class RegisterDTo
    {
        
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string UserName { get; set; }

        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
