// Liam Kandel
namespace ShopAppLib.Models
{
    public class Item
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value >= 0 ? value : 0; }
        }

        public int Id { get; set; }

        private int stock;
        public int Stock
        {
            get { return stock; }
            set { stock = value >= 0 ? value : 0; }
        }
        public override string ToString()
        {
            return $"{Id,-5}\t{Name,-10}\t{Price,-10}\t{Stock,-10}";
        }

        public Item()
        {
        }
    }
}
