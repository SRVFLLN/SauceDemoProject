using OpenQA.Selenium;

namespace SauceDemoProject.Pages
{
    public class CheckoutCompletePage : Form
    {
        public CheckoutCompletePage() : base(By.XPath("//span[contains(text(),'Complete!')]"), "Checkout Complete page") { }

        public NavigationForm NavigationFormActions => NavigationForm.NavigationFormActions;
    }
}
