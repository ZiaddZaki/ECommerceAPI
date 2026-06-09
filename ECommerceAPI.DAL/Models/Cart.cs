namespace ECommerceAPI.DAL
{
    public class Cart
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; } = new HashSet<CartProduct>();
    }
}
