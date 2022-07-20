using OpenQA.Selenium;
using SauceDemoProject.Elements;

namespace SauceDemoProject.Pages
{
    public class OverviewPage : Form
    {
        private static readonly TextBox totalPrice = new TextBox(By.ClassName("summary_subtotal_label"), "Item total price textbox");
        private static readonly Button finishButton = new Button(By.Id("finish"), "Finish button"); 
        private static readonly TextBox productContainers = new TextBox(By.ClassName("cart_item"), "Product container");

        public OverviewPage() : base(By.XPath("//span[contains(text(),'Overview')]"), "Overview page") { }

        public NavigationForm NavigationFormActions = NavigationForm.NavigationFormActions;

        /// <summary>
        /// Get count of ordered products list.
        /// </summary>
        /// <returns>List elements count</returns>
        public int GetListCount() => SingletonDriver.Source.FindElements(productContainers.Locator).Count;

        public string GetPriceValue() => totalPrice.Text;

        public void ClickFinishButton() => finishButton.Click();
    }
}
