using NUnit.Framework;
using SauceDemoProject.Pages;
using SauceDemoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SauceDemoProject
{
    public class Tests : BaseTest
    {
        //Test case in Trello:
        //https://trello.com/c/4J8E4hoW
        [Test]
        public void LoginWithCorrectParameters()
        {
            var loginPage = new LoginPage();
            Assert.IsTrue(loginPage.IsPageOpen, "Login page not opened");
            loginPage.LogIn("standard_user", ConfigTool.GetTagValue("password"));
            var productsPage = new ProductsPage();
            Assert.IsTrue(productsPage.IsPageOpen);
            productsPage.NavigationFormActions.LogOut();
            Assert.IsTrue(loginPage.IsPageOpen, "Login page not opened");
        }

        //Test case in Trello:
        //https://trello.com/c/yU5ZiOqM
        [Test]
        public void LoginWithIncorrectParameters()
        {
            var loginPage = new LoginPage();
            Assert.IsTrue(loginPage.IsPageOpen, "Login page not opened");
            loginPage.LogIn("user", ConfigTool.GetTagValue("password") + '1');
            Assert.IsTrue(loginPage.IsLoginErrorMsgPresent(), "Message about incorrect login information not displayed");
        }

        //Test case in Trello:
        //https://trello.com/c/SDdpz3YH
        [Test]
        public void LoginWithDataOfLockedOutUser()
        {
            var loginPage = new LoginPage();
            Assert.IsTrue(loginPage.IsPageOpen, "Login page not opened");
            loginPage.LogIn("locked_out_user", ConfigTool.GetTagValue("password"));
            Assert.IsTrue(loginPage.IsLockedOutMsgPresent(), "Message about user is locked out not displayed");
        }

        private static int[] Products => new int[]{0,1,2,3,4,5};

        //Test case in Trello:
        //https://trello.com/c/E9qUbfmf
        [Test, TestCaseSource("Products")]
        public void ProductInfoMatch(int productId) 
        {
            var loginPage = new LoginPage();
            Assert.IsTrue(loginPage.IsPageOpen, "Login page not opened");
            loginPage.LogIn("standard_user", ConfigTool.GetTagValue("password"));
            var productsPage = new ProductsPage();
            Assert.IsTrue(productsPage.IsPageOpen, "Product page not opened");
            var productInList = new Product()
            {
                Name = productsPage.GetProductName(productId),
                Discription = productsPage.GetProductDescription(productId),
                Price = float.Parse(productsPage.GetProductPrice(productId).Trim('$')),
                ImageLink = productsPage.GetProductImageLink(productId),
                Id = productId
            };
            productsPage.OpenProductPage(productId);
            var itemPage = new ItemPage();
            Assert.IsTrue(itemPage.IsPageOpen, "Product page not opened");
            var productOnPage = new Product()
            {
                Name = itemPage.GetProductName(),
                Discription = itemPage.GetProductDescription(),
                Price = float.Parse(itemPage.GetProductPrice().Trim('$')),
                ImageLink = itemPage.GetProductImageLink(),
                Id = null
            };
            Assert.AreEqual(productInList, productOnPage, "Product info not match");
            itemPage.BackToProductsPage();
            Assert.IsTrue(productsPage.IsPageOpen, "Product page not opened");
        }

        private static int[] IdSource => RandomTool.GetRandomNumbers(new Random().Next(2, 5),0,5);
        //Test case in Trello:
        //https://trello.com/c/oGCy37UL
        [Test]
        public void AddingAndRemovingItems()
        {
            int[] productsId = IdSource;
            var loginPage = new LoginPage();
            Assert.IsTrue(loginPage.IsPageOpen, "Login page not opened");
            loginPage.LogIn("standard_user", ConfigTool.GetTagValue("password"));
            var productsPage = new ProductsPage();
            Assert.IsTrue(productsPage.IsPageOpen, "Products page not opened");
            for (int i = 0; i < productsId.Length; i++)
            {
                productsPage.ClickProductButton(productsId[i]);
            }
            Assert.AreEqual(productsId.Length, productsPage.NavigationFormActions.GetProductsCount(productsId.Length), "Wrong count of added items");
            for (int i = 0; i < productsId.Length; i++)
            {
                productsPage.ClickProductButton(productsId[i]);
            }
            Assert.Zero(productsPage.NavigationFormActions.GetProductsCount(0), "Wrong count of added items");
            Assert.Multiple(() =>
            {
                for (int i = 0; i < productsId.Length; i++)
                {
                    var productName = productsPage.GetProductName(productsId[i]);
                    productsPage.OpenProductPage(productsId[i]);
                    var itemPage = new ItemPage();
                    Assert.IsTrue(itemPage.IsPageOpen, $"{productName} product page not opened");
                    itemPage.ClickProductButton();
                    Assert.AreEqual(1, itemPage.NavigationFormActions.GetProductsCount(1), "Product counter shows wrong count");
                    itemPage.ClickProductButton();
                    Assert.Zero(itemPage.NavigationFormActions.GetProductsCount(0));
                    itemPage.BackToProductsPage();
                    Assert.IsTrue(productsPage.IsPageOpen, "Products page not opened");
                    Assert.Zero(productsPage.NavigationFormActions.GetProductsCount(0), "Product counter shows wrong count");
                }
            });
            List<Product> products = new List<Product>();
            for (int i = 0; i < productsId.Length; i++)
            {
                products.Add(new Product()
                {
                    Name = productsPage.GetProductName(productsId[i]),
                    Discription = productsPage.GetProductDescription(productsId[i]),
                    Price = float.Parse(productsPage.GetProductPrice(productsId[i]).Trim('$')),
                    Id = productsId[i]
                });
                productsPage.ClickProductButton(productsId[i]);
            }
            Assert.AreEqual(productsId.Length, productsPage.NavigationFormActions.GetProductsCount(productsId.Length), "Product counter shows wrong count");
            productsPage.NavigationFormActions.OpenCartPage();
            var cartPage = new CartPage();
            Assert.IsTrue(cartPage.IsPageOpen, "Cart page not opened");
            Assert.AreEqual(productsId.Length, cartPage.GetListCount(), "List count not correct");
            Assert.Multiple(() =>
            {
                for (int i = 0; i < productsId.Length; i++)
                {
                    Product product = (from prod in products where prod.Id == productsId[i] select prod).First();
                    Assert.AreEqual(product, new Product()
                    {
                        Name = cartPage.GetProductName(productsId[i]),
                        Discription = cartPage.GetProductDescription(productsId[i]),
                        Price = float.Parse(cartPage.GetProductPrice(productsId[i]).Trim('$')),
                        Id = productsId[i]
                    }, "Product in list not match with product on Products page.");
                    cartPage.ClickProductButton(productsId[i]);
                }
            });
            Assert.Zero(cartPage.GetListCount(), "Products not removed from product list on cart page");
        }
    }
}