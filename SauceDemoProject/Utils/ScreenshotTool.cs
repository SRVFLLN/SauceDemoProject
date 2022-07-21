using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace SauceDemoProject.Utils
{
    public static class ScreenshotTool
    {
        private static string filename;
        private static string saveLocation;

        private static void CreateTempDirectory()
        {
            try
            {
                saveLocation = "../../../TestsArtifacts/";
                bool dirExists = System.IO.Directory.Exists(saveLocation);
                if (!dirExists)
                    System.IO.Directory.CreateDirectory(saveLocation);
            }
            catch (Exception) { }
        }

        private static void CreateFilename()
        {
            try
            {
                var timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                filename = TestContext.CurrentContext.Test.FullName.Replace("\"","'");
                string ext = ".png";
                filename += timeStamp;
                filename = saveLocation + filename + ext;
            }
            catch (Exception) { }
        }

        public static void GetScreenshot(IWebDriver driver)
        {
            try
            {
                CreateTempDirectory();
                CreateFilename();
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(filename, ScreenshotImageFormat.Png);
            }
            catch (Exception) { }
        }
    }
}
