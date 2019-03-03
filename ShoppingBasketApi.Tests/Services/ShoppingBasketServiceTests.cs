using NUnit.Framework;
using ShoppingBasketApi.Models;
using ShoppingBasketApi.Repositories;
using ShoppingBasketApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasketApi.Services.Tests
{



    [TestFixture]
    public class ShoppingBasketServiceTests
    {

        private IShoppingBasketService _shoppingBasketService;

        [SetUp]
        public void Init()
        {
            _shoppingBasketService = new ShoppingBasketService(new ShoppingBasketRepository());

        }

        [Test]
        public void GetBasketForCustomer_WhenBasketExistsForACustomer_ThenOneBasketWithItemsIsReturned()
        {
            var basket = _shoppingBasketService.GetBasketForCustomer(1);

            Assert.IsNotNull(basket);
            Assert.AreEqual(basket.CustomerId, 1);
        }

        [Test]
        public void GetBasketForCustomer_WhenThereIsNoBasketForACustomer_ThenNullIsReturned()
        {
            var basket = _shoppingBasketService.GetBasketForCustomer(0);

            Assert.IsNull(basket);
        }

        [Test]
        public void AddItemToBasket_WhenAddingAnItemToABasket_AndBasketExists_AndTheItemDoesNotExists_ThenTheItemIsAddedToTheBasket()
        {

            var basketItemsCount = _shoppingBasketService.GetBasketForCustomer(1).OrderItems.Count();

            var item = new ShoppingBasketItem
            {
                ProductId = 677,
                Price = 15,
                ProductName = "The Art of Unit Test Book",
                Quantity = 1
            };

            _shoppingBasketService.AddItemToBasket(1, item);

            var basket = _shoppingBasketService.GetBasketForCustomer(1);

            Assert.AreEqual(basketItemsCount + 1, basket.OrderItems.Count());
            Assert.IsTrue(basket.OrderItems.Exists(i => i.ProductId == 677));
        }

        [Test]
        public void AddItemToBasket_WhenAddingAnItemToABasket_AndBasketExists_AndTheItemExists_ThenTheItemQuantityIsIncreasedByQuantityValueInNewItem()
        {


            var item = new ShoppingBasketItem
            {
                ProductName = "Golden Son Book",
                ProductId = 888,
                Price = 15.00m,
                Quantity = 1
            };

            _shoppingBasketService.AddItemToBasket(333, item);

            var items = _shoppingBasketService.GetBasketForCustomer(333).OrderItems;
            var basketItemsCount = items.Count();


            var secondItem = new ShoppingBasketItem
            {
                ProductName = "Golden Son Book",
                ProductId = 888,
                Price = 15.00m,
                Quantity = 2
            };

            _shoppingBasketService.AddItemToBasket(333, secondItem);

            var basket = _shoppingBasketService.GetBasketForCustomer(333);

            Assert.AreEqual(basketItemsCount, basket.OrderItems.Count());
            Assert.IsTrue(basket.OrderItems.Exists(i => i.ProductId == 888));
            Assert.AreEqual(basket.OrderItems.First(i => i.ProductId == 888).Quantity, 3);
        }


        [Test]
        public void AddItemToBasket_WhenAddingAnItemToABasket_AndBasketDoesNotExists_ThenNewBasketIsCreated_AndTheItemIsAddedToTheBasket()
        {

            var notExistingBasket = _shoppingBasketService.GetBasketForCustomer(999);

            var item = new ShoppingBasketItem
            {
                ProductId = 677,
                Price = 15,
                ProductName = "The Art of Unit Test Book",
                Quantity = 1
            };

            _shoppingBasketService.AddItemToBasket(999, item);

            var basket = _shoppingBasketService.GetBasketForCustomer(999);

            Assert.IsNull(notExistingBasket);
            Assert.IsNotNull(basket);
            Assert.AreEqual(basket.OrderItems.Count(), 1);
            Assert.IsTrue(basket.OrderItems.Exists(i => i.ProductId == 677));
        }

        [Test]
        public void RemoveItemFromBasket_WhenRemovingItemFromBasket_AndBasketExists_AndItemExists_AndBasketHasAtLeast2Items_ThenItemIsRemovedFromBasket()
        {

            var item = new ShoppingBasketItem
            {
                ProductId = 677,
                Price = 15,
                ProductName = "The Art of Unit Test Book",
                Quantity = 1
            };

            _shoppingBasketService.AddItemToBasket(444, item);

            var secondItem = new ShoppingBasketItem
            {
                ProductName = "Golden Son Book",
                ProductId = 888,
                Price = 15.00m,
                Quantity = 2
            };

            var itemToDeleteId = _shoppingBasketService.AddItemToBasket(444, secondItem);

            var basket = _shoppingBasketService.GetBasketForCustomer(444);

            var itemsBeforeDelete = basket.OrderItems.ToList();

            var countBeforeDelete = itemsBeforeDelete.Count();

            _shoppingBasketService.RemoveItemFromBasket(444, itemToDeleteId);


            Assert.AreEqual(countBeforeDelete, 2);
            Assert.IsTrue(itemsBeforeDelete.Exists(i => i.ProductId == 888));
            Assert.AreEqual(basket.OrderItems.Count(), 1);
            Assert.IsFalse(basket.OrderItems.Exists(i => i.ProductId == 888));
        }

        [Test]
        public void RemoveItemFromBasket_WhenRemovingItemFromBasket_AndBasketExists_AndItemExists_AndBasketHasOnly1Item_ThenBasketIsRemoved()
        {

            var item = new ShoppingBasketItem
            {
                ProductId = 677,
                Price = 15,
                ProductName = "The Art of Unit Test Book",
                Quantity = 1
            };


            var itemToDeleteId = _shoppingBasketService.AddItemToBasket(555, item);

            _shoppingBasketService.RemoveItemFromBasket(555, itemToDeleteId);

            var basket = _shoppingBasketService.GetBasketForCustomer(555);


            Assert.IsNull(basket);

        }

        [Test]
        public void UpdateItemOnBasket_WhenUpdatingItem_AndBasketExists_AndItemExists_AndNewQuantityGreaterThanZero_ThenItemQuantityIsUpdated()
        {

            var item = new ShoppingBasketItem
            {
                ProductId = 677,
                Price = 15,
                ProductName = "The Art of Unit Test Book",
                Quantity = 1
            };


            var itemId = _shoppingBasketService.AddItemToBasket(666, item);

            _shoppingBasketService.UpdateItemOnBasket(666, itemId, 5);

            var basket = _shoppingBasketService.GetBasketForCustomer(666);


            Assert.AreEqual(basket.OrderItems.First(i=>i.ItemId == itemId).Quantity,5);

        }

        [Test]
        public void UpdateItemOnBasket_WhenUpdatingItem_AndBasketExists_AndItemExists_AndNewQuantityIsZero_ThenItemIsRemoved()
        {

            var item = new ShoppingBasketItem
            {
                ProductId = 677,
                Price = 15,
                ProductName = "The Art of Unit Test Book",
                Quantity = 1
            };


            var itemId = _shoppingBasketService.AddItemToBasket(777, item);


            var secondItem = new ShoppingBasketItem
            {
                ProductName = "Golden Son Book",
                ProductId = 888,
                Price = 15.00m,
                Quantity = 2
            };

            _shoppingBasketService.AddItemToBasket(777, secondItem);

            _shoppingBasketService.UpdateItemOnBasket(777, itemId, 0);

            var basket = _shoppingBasketService.GetBasketForCustomer(777);


            Assert.AreEqual(basket.OrderItems.Count(), 1);
            Assert.IsFalse(basket.OrderItems.Exists(i=>i.ItemId == itemId));

        }


        [Test]
        public void ClearBasket_WhenClearingABasket_AndBasketExist_ThenBasketIsRemovedFromTheRepository()
        {

            var item = new ShoppingBasketItem
            {
                ProductId = 677,
                Price = 15,
                ProductName = "The Art of Unit Test Book",
                Quantity = 1
            };

            _shoppingBasketService.AddItemToBasket(9999, item);

            var basket = _shoppingBasketService.GetBasketForCustomer(9999);

            _shoppingBasketService.ClearBasket(9999);

            var notExistingBasket = _shoppingBasketService.GetBasketForCustomer(9999);

            Assert.IsNotNull(basket);
            Assert.IsNull(notExistingBasket);
        }


    }
}