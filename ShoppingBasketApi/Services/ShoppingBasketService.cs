using ShoppingBasketApi.Models;
using ShoppingBasketApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;

namespace ShoppingBasketApi.Services
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly IShoppingBasketRepository _repository;


        public ShoppingBasketService(IShoppingBasketRepository repository)
        {
            _repository = repository;
        }

        public ShoppingBasket GetBasketForCustomer(int customerId)
        {
            return _repository.GetBasket(customerId);
        }      

        public int AddItemToBasket(int customerId, ShoppingBasketItem item)
        {
            var basket = _repository.GetBasket(customerId);

            if (basket != null)
            {
                if (basket.OrderItems.Exists(i => i.ProductId == item.ProductId))
                {
                    var existingItem = basket.OrderItems.First(i => i.ProductId == item.ProductId);
                    var quantity = existingItem.Quantity + item.Quantity;
                    _repository.UpdateShoppingBasketItem(existingItem, quantity);
                    return existingItem.ItemId;
                }
                else
                {
                   return _repository.InsertShoppingBasketItem(basket, item);
                }
            }
            else
            {
                var newBasket = new ShoppingBasket
                {
                    CustomerId = customerId
                };              

                _repository.InsertBasket(newBasket);

                var existingBasket = _repository.GetBasket(customerId);
                return _repository.InsertShoppingBasketItem(existingBasket, item);
            }
        }

        public void RemoveItemFromBasket(int customerId, int itemId)
        {
            var basket = _repository.GetBasket(customerId);

            if (basket != null)
            {
                if (basket.OrderItems.Count > 1)
                {
                    var item = _repository.GetShoppingBasketItem(basket, itemId);
                    _repository.RemoveShoppingBasketItem(basket, item);
                }
                else
                    _repository.RemoveBasket(basket);
            }
        }

        public void UpdateItemOnBasket(int customerId, int itemId, int quantity)
        {
            var basket = _repository.GetBasket(customerId);

            if (basket != null)
            {
                var item = _repository.GetShoppingBasketItem(basket, itemId);

                if (quantity > 0)
                    _repository.UpdateShoppingBasketItem(item, quantity);
                else
                    _repository.RemoveShoppingBasketItem(basket,item);
            }
        }

        public void ClearBasket(int customerId)
        {
            var basket = _repository.GetBasket(customerId);
            if (basket != null)
                _repository.RemoveBasket(basket);
        }
    }
}