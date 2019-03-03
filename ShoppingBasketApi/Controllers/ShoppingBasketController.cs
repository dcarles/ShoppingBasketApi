using ShoppingBasketApi.Models;
using ShoppingBasketApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ninject;

namespace ShoppingBasketApi.Controllers
{
    public class ShoppingBasketController : ApiController
    {
        private readonly IShoppingBasketService _shoppingBasketService;

        public ShoppingBasketController(IShoppingBasketService shoppingBasketService)
        {
            _shoppingBasketService = shoppingBasketService;
        }

        /// <summary>
        /// Return the shopping basket for a particular customer
        /// </summary>
        /// <param name="customerId">The id of the customer</param>
        /// <returns>Shopping basket with list of items</returns>
        [Route("ShoppingBasket/{customerId}/", Name = "List")]
        [HttpGet]
        public HttpResponseMessage Get(int customerId)
        {
            var basket = _shoppingBasketService.GetBasketForCustomer(customerId);

            if (basket != null)
                return Request.CreateResponse(HttpStatusCode.OK, basket);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound,"Customer does not have any Shopping Basket");

        }

        /// <summary>
        /// Clear All the items from a Customer's basket
        /// </summary>
        /// <param name="customerId">The id of the customer</param>     
        [Route("ShoppingBasket/{customerId}/Clear", Name = "Clear")]
        [HttpPost]
        public void ClearBasket(int customerId)
        {
            _shoppingBasketService.ClearBasket(customerId);
        }

        /// <summary>
        /// Add a new item to a Customer's basket
        /// </summary>
        /// <param name="customerId">The id of the customer</param>
        /// <param name="newItem">The item to add to the basket</param>     
        [Route("ShoppingBasket/{customerId}/Item", Name = "AddItem")]
        [HttpPost]
        public void AddItemToBasket(int customerId, [FromBody] ShoppingBasketItemInsertVM newItem)
        {
            var item = new ShoppingBasketItem(newItem);
            _shoppingBasketService.AddItemToBasket(customerId, item);
        }


        /// <summary>
        ///  Update quantity of an item in the basket. If Quantity is Zero the item will be deleted from the basket
        /// </summary>
        /// <param name="customerId">The id of the customer</param>
        /// <param name="itemId">The id of the item to modify</param>
        /// <param name="item">The item to be updated including the new quantity</param>
        [Route("ShoppingBasket/{customerId}/Item/{itemId}", Name = "UpdateItem")]
        [HttpPut]
        public void UpdateItemQuantity(int customerId, int itemId, [FromBody] ShoppingBasketItemUpdateVM item)
        {
            _shoppingBasketService.UpdateItemOnBasket(customerId, itemId, item.Quantity);
        }

        /// <summary>
        /// Remove an item from the shopping basket. 
        /// </summary>
        /// <param name="customerId">The id of the customer</param>
        /// <param name="itemId">The id of the item to modify</param>
        [HttpDelete]
        [Route("ShoppingBasket/{customerId}/Item/{itemId}", Name = "DeleteItem")]
        public void RemoveItemFromBasket(int customerId, int itemId)
        {
            _shoppingBasketService.RemoveItemFromBasket(customerId, itemId);
        }
    }
}
