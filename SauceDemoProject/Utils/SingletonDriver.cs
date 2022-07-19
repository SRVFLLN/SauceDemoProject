using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.IO;

namespace SauceDemoProject
{
    public class SingletonDriver
    {
        private static IWebDriver driver = null;

        private SingletonDriver() 
        {
            Logger.Info("Starting browser...");
            driver = new ChromeDriver(Directory.GetCurrentDirectory() + "/Resources"); 
        }

        public static IWebDriver Source 
        {
            get 
            {  
                if (driver == null) 
                {
                    new SingletonDriver();
                }
                return driver;
            }
        }

        public static void Quit() 
        {
            driver.Quit();
            driver = null;
            Logger.Info("Close browser...");
        }
    }
}
