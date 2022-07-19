using OpenQA.Selenium;
using SauceDemoProject.Elements;

namespace SauceDemoProject.Pages
{
    public class NavigationForm
    {
        private static readonly Button menuButton = new Button(By.Id("react-burger-menu-btn"), "Menu button on Navigation form");
        private static readonly Link cartLink = new Link(By.ClassName("shopping_cart_link"), "Cart link on Navigation form");
        private static readonly TextBox orderedProductsCounter = new TextBox(By.ClassName("shopping_cart_badge"), "Ordered products counter on Navigation form");
        private static readonly Button logOutButtonInMenu = new Button(By.Id("logout_sidebar_link"), "Logout button in navigation menu");
        private static NavigationForm form = null;
        private NavigationForm() { }

        public static NavigationForm NavigationFormActions 
        {
            get 
            {
                if (form == null) 
                {
                    form = new NavigationForm();
                }
                return form;
            }
        }

        /// <summary>
        /// Log out from present account.
        /// </summary>
        public void LogOut()
        {
            menuButton.Click();
            logOutButtonInMenu.Click();
        }

        public void OpenCartPage() => cartLink.Click();

        /// <summary>
        /// Get count of ordered products.
        /// </summary>
        /// <returns>Ordered products count.</returns>
        public int GetProductsCount(int? value = null) 
        {
            try
            {
                if (value == null)
                {
                    var txt = orderedProductsCounter.Text;
                    Waitings.WaitUntilCondition(drv => SingletonDriver.Source.FindElement(orderedProductsCounter.Locator).Text != txt);
                    return int.Parse(orderedProductsCounter.Text);
                }
                else if (value == 0) 
                {
                    Waitings.WaitForElementIsNotDisplayed(orderedProductsCounter);
                    return 0;
                }
                else
                {
                    Waitings.WaitForElementIsDisplayed(orderedProductsCounter);
                    Waitings.WaitUntilCondition(drv => SingletonDriver.Source.FindElement(orderedProductsCounter.Locator).Text == value.ToString());
                    return int.Parse(orderedProductsCounter.Text);
                }
            }
            catch(WebDriverTimeoutException) 
            {
                return 0;
            }
        }
    }
}
