using ShoppingBasketApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingBasketApi.Repositories
{
    public class ShoppingBasketRepository : IShoppingBasketRepository
    {

        private static List<ShoppingBasket> _baskets;


        static ShoppingBasketRepository()
        {

            var basketDefault = new ShoppingBasket
            {
                BasketId = 1,
                CustomerId = 1
            };

            basketDefault.OrderItems.Add(new ShoppingBasketItem
            {
                ItemId = 1,
                ProductName = "Red Rising Hardcover Book",
                ProductId = 123,
                Price = 15.00m,
                Quantity = 1
            });

            basketDefault.OrderItems.Add(new ShoppingBasketItem
            {
                ItemId = 2,
                ProductName = "Lord Of The Rings",
                ProductId = 456,
                Price = 15.00m,
                Quantity = 1
            });

            _baskets = new List<ShoppingBasket>();
            _baskets.Add(basketDefault);
        }


        public ShoppingBasket GetBasket(int customerId)
        {
            return _baskets.FirstOrDefault(b => b.CustomerId == customerId);
        }

        public void InsertBasket(ShoppingBasket basket)
        {
            basket.BasketId = _baskets.Count() > 0 ? _baskets.Max(b => b.BasketId) + 1 : 1;
            _baskets.Add(basket);
        }

        public void RemoveBasket(ShoppingBasket basket)
        {
            _baskets.Remove(basket);
        }

        public int InsertShoppingBasketItem(ShoppingBasket basket, ShoppingBasketItem item)
        {
            item.ItemId = _baskets.Count() > 0 ? (_baskets.SelectMany(b => b.OrderItems).Max(i => i.ItemId)) + 1 : 1;
            basket.OrderItems.Add(item);

            return item.ItemId;
        }

        public void RemoveShoppingBasketItem(ShoppingBasket basket, ShoppingBasketItem item)
        {
            basket.OrderItems.Remove(item);
        }

        public void UpdateShoppingBasketItem(ShoppingBasketItem item, int quantity)
        {
            item.Quantity = quantity;
        }

        public ShoppingBasketItem GetShoppingBasketItem(ShoppingBasket basket, int itemId)
        {
            return basket.OrderItems.FirstOrDefault(i => i.ItemId == itemId);
        }

    }
}