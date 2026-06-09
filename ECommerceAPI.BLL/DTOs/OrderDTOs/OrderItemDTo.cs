using ECommerceAPI.DAL;

namespace ECommerceAPI.BLL
{
    public class OrderItemDTo
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }

    }
}
