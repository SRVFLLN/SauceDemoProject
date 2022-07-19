using OpenQA.Selenium;
using SauceDemoProject.Elements;

namespace SauceDemoProject.Pages
{
    public class CartPage : Form
    {
        private static readonly Button checkoutButton = new Button(By.Id("checkout"), "Checkout button");
        private static readonly TextBox productContainers = new TextBox(By.ClassName("cart_item"), "Product container");
        private static TextBox productName(int productId) => new TextBox(By.XPath($"//a[contains(@id,'item_{productId}')]/div"), $"Product {productId} name");
        private static TextBox productDescription(int productId) => new TextBox(By.XPath($"//a[contains(@id,'item_{productId}')]/following-sibling::div[contains(@class,'desc')]"), $"Product {productId} description");
        private static TextBox productPrice(int productId) => new TextBox(By.XPath($"//a[contains(@id,'item_{productId}')]/following-sibling::div/div"), $"Product {productId} price");
        private static Button productButton(int productId) => new Button(By.XPath($"//a[contains(@id,'item_{productId}')]/following-sibling::div/button"), $"Product {productId} Add/Remove button");

        public CartPage() : base(By.XPath("//span[text()='Your Cart']"),"Cart page") { }

        /// <summary>
        /// Get count of ordered products list.
        /// </summary>
        /// <returns>List elements count</returns>
        public int GetListCount() 
        {
            return SingletonDriver.Source.FindElements(productContainers.Locator).Count;
        }

        public string GetProductName(int productId) => productName(productId).Text;
        public string GetProductDescription(int productId) => productDescription(productId).Text;
        public string GetProductPrice(int productId) => productPrice(productId).Text;
        public void ClickProductButton(int productId) => productButton(productId).Click();

        public void OpenCheckoutPage() => checkoutButton.Click();
    }
}
