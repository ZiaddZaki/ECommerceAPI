using ECommerceAPI.DAL;

namespace ECommerceAPI.BLL
{
    public class OrderReadDTo
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDTo> Items { get; set; } = new List<OrderItemDTo>();

    }
}
