USING NUNIT V3 FOR THE UNIT TESTS

The Project is setup to Run on port 61063 and should automatically open the url -> http://localhost:61063/swagger

Added some initial data for api testing purposes:

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


So you should be able to retrieve that basket to play around in Swagger.


SOME IMPROVEMENTS I WOULD DO IF I HAD MORE TIME / IF IT WAS REAL PRODUCTION PROJECT


- I did not have time to create the Client (however I did setup swagger so that provide documentation on how to call the api and you should be able to test it). 
Creating a client should be easy just using HttpClient and then using GetAsync or PostAsJsonAsync or similar
- I would have clarify the requirements better to understand what activate a membership entitles and what is a shipping slip, etc (now i just made assumptions), 
and spend bit more time understanding what kind of rules can there be if we can design something that make it even easier to import and execute rules
- Would have using some Dynamic mock object framework (like rhinoMocks or Moq) to mock some services and repos for the tests.
- Would have implemented a proper repository rather than static list in memory
- Would have implemented more validations and checks around the Models like avoid creating objects without basic fields, etc. Also protected them more so certain fields can be modified in certain scenarios, etc
- Would have implemented better Response management in the API like I did for the Get Basket endpoint returning 404 with a message if Basket does not exists.