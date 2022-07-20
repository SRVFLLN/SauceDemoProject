using OpenQA.Selenium;
using SauceDemoProject.Elements;
using SauceDemoProject.Models;
using System.Collections.Generic;
using SauceDemoProject.Utils;

namespace SauceDemoProject.Pages
{
    public class ProductsPage : Form
    {
        private static readonly TextBox productSort = new TextBox(By.ClassName("product_sort_container"), "Product sort continer");
        private static readonly TextBox nameOfOrder = new TextBox(By.ClassName("active_option"), "Name of selected ordered option");
        private static readonly Button orderAZ = new Button(By.XPath("//option[@value='az']"), "Order by alphabet A to Z");
        private static readonly Button orderZA = new Button(By.XPath("//option[@value='za']"), "Order by alphabet Z to A");
        private static readonly Button orderLoHi = new Button(By.XPath("//option[@value='lohi']"), "Order by price low to high");
        private static readonly Button orderHiLo = new Button(By.XPath("//option[@value='hilo']"), "Order by price high to low");
        private static readonly TextBox productsNames = new TextBox(By.ClassName("inventory_item_name"), "Products names");
        private static readonly TextBox productsPrices = new TextBox(By.ClassName("inventory_item_price"), "Products prices");

        private static Link productImageLink(int productId) => new Link(By.XPath($"//div[@class='inventory_item']//a[@id='item_{productId}_img_link']//img"), $"Product {productId} image link");

        private static Link productName(int productId) => new Link(By.XPath($"//div[@class='inventory_item']//a[@id='item_{productId}_title_link']//div"), $"Product {productId} name");

        private static TextBox productDescription(int productId) => new TextBox(By.XPath($"//div[@class='inventory_item']//a[@id='item_{productId}_title_link']/following-sibling::div"), $"Product {productId} description");

        private static TextBox productPrice(int productId) => new TextBox(By.XPath($"//div[@class='inventory_item']//a[@id='item_{productId}_title_link']/parent::div//following-sibling::div/div"), $"Product {productId} price");

        private static Button productButton(int productId) => new Button(By.XPath($"//div[@class='inventory_item']//a[@id='item_{productId}_title_link']/parent::div//following-sibling::div/button"), $"Product {productId} Add/Remove button");

        public ProductsPage() : base(By.XPath("//span[@class='title' and text()='Products']"), "Products page") { }

        public NavigationForm NavigationFormActions => NavigationForm.NavigationFormActions;

        public void SelectOrderOption(OrderOption option) 
        {
            productSort.Click();
            switch (option) 
            {
                case OrderOption.AtoZ:
                    orderAZ.Click();
                    break;
                case OrderOption.ZtoA:
                    orderZA.Click();
                    break;
                case OrderOption.LowToHigh:
                    orderLoHi.Click();
                    break;
                case OrderOption.HighToLow:
                    orderHiLo.Click();
                    break;
            }
        }

        public OrderOption GetSelectedOption() 
        {
            if (nameOfOrder.Text.ToLower().Contains("a to z")) return OrderOption.AtoZ;
            else if (nameOfOrder.Text.ToLower().Contains("z to a")) return OrderOption.ZtoA;
            else if (nameOfOrder.Text.ToLower().Contains("low to high")) return OrderOption.LowToHigh;
            else return OrderOption.HighToLow;
        }

        public List<string> GetProductsNames() 
        {
            List<string> names = new List<string>();
            var elements = SingletonDriver.Source.FindElements(productsNames.Locator);
            for (int i = 0; i < elements.Count; i++) 
            {
                names.Add(elements[i].Text);
            }
            return names;
        }

        public List<double> GetProductPrices() 
        {
            List<double> prices = new List<double>();
            var elements = SingletonDriver.Source.FindElements(productsPrices.Locator);
            for (int i = 0; i < elements.Count; i++)
            {
                prices.Add(elements[i].Text.ExtractNumber());
            }
            return prices;
        }

        public string GetProductName(int productId) => productName(productId).Text;

        public string GetProductDescription(int productId) => productDescription(productId).Text;

        public string GetProductPrice(int productId) => productPrice(productId).Text;

        public string GetProductImageLink(int productId) => productImageLink(productId).Href;

        public void ClickProductButton(int productId) 
        {
            var button = productButton(productId);
            string buttonText = button.Text;
            button.Click(drv => SingletonDriver.Source.FindElement(button.Locator).Text != buttonText);
        }

        public void OpenProductPage(int productId) => productName(productId).Click();
    }
}
