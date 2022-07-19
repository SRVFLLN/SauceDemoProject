using OpenQA.Selenium;
using SauceDemoProject.Elements;

namespace SauceDemoProject.Pages
{
    public class ItemPage : Form
    {
        private static readonly TextBox productName = new TextBox(By.XPath("//div[contains(@class,'name')]"), "Product Name");
        private static readonly TextBox productDescription = new TextBox(By.XPath("//div[contains(@class,'name')]/following-sibling::div[contains(@class,'desc')]"), "Product description");
        private static readonly TextBox productPrice = new TextBox(By.XPath("//div[contains(@class,'price')]"), "Product price");
        private static readonly Button productButton = new Button(By.XPath("//button[contains(@class,'btn_inventory')]"), "Product Add/Remove button");
        public static readonly Link productImageLink = new Link(By.ClassName("inventory_details_img"), "Product image link");
        public static readonly Button backToProducts = new Button(By.Id("back-to-products"), "Back to products button");

        public ItemPage() : base(By.ClassName("inventory_details_container"), "Item details container") { }

        public NavigationForm NavigationFormActions => NavigationForm.NavigationFormActions;

        public string GetProductName() => productName.Text;
        public string GetProductDescription() => productDescription.Text;
        public string GetProductPrice() => productPrice.Text;
        public string GetProductImageLink() => productImageLink.Href;
        public void ClickProductButton() => productButton.Click();
        public void BackToProductsPage() => backToProducts.Click();
    }
}
