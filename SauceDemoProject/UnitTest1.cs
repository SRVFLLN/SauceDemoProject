using NUnit.Framework;
using SauceDemoProject.Pages;

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
        }

        //Test case in Trello:
        //https://trello.com/c/yU5ZiOqM
        [Test]
        public void LoginWithIncorrectParameters() 
        {
            var loginPage = new LoginPage();
            Assert.IsTrue(loginPage.IsPageOpen, "Login page not opened");
            loginPage.LogIn("user", ConfigTool.GetTagValue("password")+'1');
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
    }
}