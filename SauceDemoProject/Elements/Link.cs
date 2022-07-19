using OpenQA.Selenium;

namespace SauceDemoProject.Elements
{
    public class Link : BaseElement
    {
        public Link(By locator, string name) : base(locator, name) { }

        public string Href => GetAttribute("href");
    }
}
