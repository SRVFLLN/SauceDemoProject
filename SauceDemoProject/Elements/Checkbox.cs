using OpenQA.Selenium;

namespace SauceDemoProject.Elements
{
    public class Checkbox : BaseElement
    {
        public Checkbox(By locator, string name) : base(locator, name) { }

        public bool IsSelected => FindElement().Selected;
    }
}