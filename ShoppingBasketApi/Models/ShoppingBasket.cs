using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingBasketApi.Models
{
    public class ShoppingBasket
    {

        public int BasketId { get; set; }
        public int CustomerId { get; set; }
        public List<ShoppingBasketItem> OrderItems { get; private set; }

        public decimal TotalAmount
        {
            get
            {
                decimal total = 0;

                foreach (var item in OrderItems)
                {
                    total += item.TotalPrice;
                }
            
                return total;
            }
        }

        public int NumberOfItems
        {
            get
            {
                int total = 0;
                foreach (var item in OrderItems)
                {
                    total +=  item.Quantity;
                }
                return total;
            }
        }

        public ShoppingBasket()
        {
            OrderItems = new List<ShoppingBasketItem>();
        }

    }
}