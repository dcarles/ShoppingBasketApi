using ShoppingBasketApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingBasketApi.Repositories
{
    public interface IShoppingBasketRepository
    {

        ShoppingBasket GetBasket(int customerId);

        void InsertBasket(ShoppingBasket basket);

        void RemoveBasket(ShoppingBasket basket);

        int InsertShoppingBasketItem(ShoppingBasket basket, ShoppingBasketItem item);

        void RemoveShoppingBasketItem(ShoppingBasket basket, ShoppingBasketItem item);

        void UpdateShoppingBasketItem(ShoppingBasketItem item, int quantity);

        ShoppingBasketItem GetShoppingBasketItem(ShoppingBasket basket, int itemId);
    }
}