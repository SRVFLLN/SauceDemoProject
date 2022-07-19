using OpenQA.Selenium;
using SauceDemoProject.Elements;

namespace SauceDemoProject.Pages
{
    public class ProductsPage : Form
    {
        private static Link productImageLink(int productId) => new Link(By.XPath($"//div[@class='inventory_item']//a[@id='item_{productId}_img_link']//img"), $"Product {productId} image link");
        private static Link productName(int productId) => new Link(By.XPath($"//div[@class='inventory_item']//a[@id='item_{productId}_title_link']//div"), $"Product {productId} name");
        private static TextBox productDescription(int productId) => new TextBox(By.XPath($"//div[@class='inventory_item']//a[@id='item_{productId}_title_link']/following-sibling::div"), $"Product {productId} description");
        private static TextBox productPrice(int productId) => new TextBox(By.XPath($"//div[@class='inventory_item']//a[@id='item_{productId}_title_link']/parent::div//following-sibling::div/div"), $"Product {productId} price");
        private static Button productButton(int productId) => new Button(By.XPath($"//div[@class='inventory_item']//a[@id='item_{productId}_title_link']/parent::div//following-sibling::div/button"), $"Product {productId} Add/Remove button");

        public ProductsPage() : base(By.XPath("//span[@class='title' and text()='Products']"), "Products page") { }

        public NavigationForm NavigationFormActions => NavigationForm.NavigationFormActions;

        public string GetProductName(int productId) => productName(productId).Text;
        public string GetProductDescription(int productId) => productDescription(productId).Text;
        public string GetProductPrice(int productId) => productPrice(productId).Text;
        public string GetProductImageLink(int productId) => productImageLink(productId).Href;
        public void ClickProductButton(int productId) 
        {
            var but = productButton(productId);
            string txt = but.Text;
            but.Click(drv => SingletonDriver.Source.FindElement(but.Locator).Text != txt);
        }
        public void OpenProductPage(int productId) => productName(productId).Click();
    }
}
