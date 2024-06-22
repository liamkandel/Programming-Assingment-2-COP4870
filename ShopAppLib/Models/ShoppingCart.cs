namespace ShopAppLib.Models
{
    public class ShoppingCart
    {
        int Id { get; set; }
        public List<Item>? Contents { get; set; }

        ShoppingCart()
        {
        }
    }
}
