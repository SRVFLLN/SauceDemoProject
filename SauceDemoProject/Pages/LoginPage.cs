using OpenQA.Selenium;
using SauceDemoProject.Elements;

namespace SauceDemoProject.Pages
{
    public class LoginPage : Form
    {
        private static readonly InputElement usernameInput = new InputElement(By.Id("user-name"), "Username input element");
        private static readonly InputElement passwordInput = new InputElement(By.Id("password"), "Password input element");
        private static readonly Button loginButton = new Button(By.Id("login-button"), "Login button");
        private static readonly TextBox loginErrorMessage = new TextBox(By.XPath("//h3[@data-test='error' and contains(text(),'Username and password do not match any user in this service')]"), "Log in error message");
        private static readonly TextBox lockedOutMessage = new TextBox(By.XPath("//h3[@data-test='error' and contains(text(),'this user has been locked out')]"), "Locke out message");

        public LoginPage() : base(By.ClassName("bot_column"),"Login page") { }

        public void LogIn(string username, string password) 
        {
            usernameInput.SendKeys(username);
            passwordInput.SendKeys(password);
            loginButton.Click();
        }

        public bool IsLoginErrorMsgPresent() => loginErrorMessage.IsDisplayed;

        public bool IsLockedOutMsgPresent() => lockedOutMessage.IsDisplayed;
    }
}
