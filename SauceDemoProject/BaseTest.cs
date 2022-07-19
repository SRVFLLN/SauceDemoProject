using NUnit.Framework;

namespace SauceDemoProject
{
    public class BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            SingletonDriver.Source.Navigate().GoToUrl(ConfigTool.GetTagValue("startUrl"));
        }

        [TearDown]
        public void TearDown() 
        {
            SingletonDriver.Quit();
        }
    }
}
