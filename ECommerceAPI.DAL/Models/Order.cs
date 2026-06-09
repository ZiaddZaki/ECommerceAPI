namespace ECommerceAPI.DAL
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; } 
        public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();

    }
}
