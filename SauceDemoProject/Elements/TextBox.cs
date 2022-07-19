using OpenQA.Selenium;

namespace SauceDemoProject.Elements
{
    public class TextBox : BaseElement
    {
        public TextBox(By locator, string name) : base(locator, name) { }
    }
}
