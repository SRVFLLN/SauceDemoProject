using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using SauceDemoProject.Elements;

namespace SauceDemoProject
{
    public static class Waitings
    {
        private static readonly WebDriverWait wait = new WebDriverWait(SingletonDriver.Source, TimeSpan.FromSeconds(1));

        public static void WaitForElementIsNotDisplayed(BaseElement element) 
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(400);
            wait.Until(drv => SingletonDriver.Source.FindElement(element.Locator).Displayed == false);
        }

        public static void WaitForElementIsDisplayed(BaseElement element) 
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(400);
            wait.Until(drv => SingletonDriver.Source.FindElement(element.Locator).Displayed == true);
        }

        public static void WaitUntilElementBeClickable(BaseElement element) 
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(400);
            wait.Until(drv => SingletonDriver.Source.FindElement(element.Locator).Selected == true);
        }

        public static void WaitUntilPageIsOpen(By locator) 
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(400);
            wait.Until(drv => SingletonDriver.Source.FindElement(locator).Displayed == true);
        }
    }
}
