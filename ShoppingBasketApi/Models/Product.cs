using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingBasketApi.Models
{
    public class Product
    {

        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public ProductCategory Category { get; set; }
    }

    public enum ProductCategory
    {
        Book,
        DVDAndBluRay,
        Grocery,
        HealthAndBeauty,
        MusicCD,
        Electronics
    }
}