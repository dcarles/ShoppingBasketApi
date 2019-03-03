using ShoppingBasketApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasketApi.Services
{
    public interface IShoppingBasketService
    {

        ShoppingBasket GetBasketForCustomer(int customerId);
   
        void ClearBasket(int customerId);

        int AddItemToBasket(int customerId, ShoppingBasketItem item);

        void RemoveItemFromBasket(int customerId, int itemId);

        void UpdateItemOnBasket(int customerId, int itemId, int quantity);

    }
}
