namespace ShoppingBasketApi.Models
{

    public class ShoppingBasketItem
    {
        
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get { return Price * Quantity; } }
        public string ProductName { get; set; }
        public int ProductId { get; set; }

        public ShoppingBasketItem(ShoppingBasketItemInsertVM item)
        {

            Quantity = item.Quantity;
            Price = item.Price;
            ProductName = item.ProductName;
            ProductId = item.ProductId;

        }
        public ShoppingBasketItem()
        {

        }
    }
}