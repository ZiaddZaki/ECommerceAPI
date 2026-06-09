namespace ECommerceAPI.BLL
{
    public class CartReadDTo
    {
        public int CartId { get; set; }

        public List<CartItemDTO> CartItems { get; set; } = new List<CartItemDTO>();
    }
}
