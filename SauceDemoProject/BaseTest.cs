using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SauceDemoProject.Utils;
using System;

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
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                ScreenshotTool.GetScreenshot(SingletonDriver.Source);
                Console.WriteLine($"Check out why the test failed, look here: TestArtifacts/{TestContext.CurrentContext.Test.FullName}.png" );
            }
            SingletonDriver.Quit();
        }
    }
}
