using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.DAL
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Order>? Orders { get; set; } = new HashSet<Order>();

    }
}
