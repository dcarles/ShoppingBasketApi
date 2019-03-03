namespace ShoppingBasketApi.Models
{

    public class ShoppingBasketItemInsertVM
    {       
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
    }
}