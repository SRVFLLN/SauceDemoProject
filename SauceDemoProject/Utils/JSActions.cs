using OpenQA.Selenium;

namespace SauceDemoProject
{
    public sealed class JSActions
    {
        public void ScrollToBottom()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)SingletonDriver.Source;
            js.ExecuteScript("window.scrollTo(0,document.body.scrollHeight);");
        }
    }
}
