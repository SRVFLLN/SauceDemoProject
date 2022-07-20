using OpenQA.Selenium;
using SauceDemoProject.Elements;

namespace SauceDemoProject.Pages
{
    public class CheckoutInfoPage : Form
    {
        private static readonly InputElement fNameInput = new InputElement(By.Id("first-name"), "First name input element");
        private static readonly InputElement lNameInput = new InputElement(By.Id("last-name"), "Last name input element");
        private static readonly InputElement zipCodeInput = new InputElement(By.Id("postal-code"), "Last name input");
        private static readonly Button continueButton = new Button(By.Id("continue"), "Continue button");

        public CheckoutInfoPage() : base(By.XPath("//span[contains(text(),' Your Information')]"), "Chekout page with info form") { }

        public NavigationForm NavigationFormActions => NavigationForm.NavigationFormActions;

        public void FillInfoForm(string fName, string lName, string zipCode) 
        {
            fNameInput.SendKeys(fName);
            lNameInput.SendKeys(lName);
            zipCodeInput.SendKeys(zipCode);
        }

        public void ClickOnContinueButton() => continueButton.Click();
    }
}
