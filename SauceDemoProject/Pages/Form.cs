using OpenQA.Selenium;

namespace SauceDemoProject.Pages
{
    public abstract class Form
    {
        private readonly By _locator;
        private readonly string _pagename;
        private readonly JSActions actions = new JSActions();
        public JSActions PageActions => actions;

        public Form(By locator, string pagename)
        {
            _locator = locator;
            _pagename = pagename;
        }

        public bool IsPageOpen 
        { 
            get 
            {
                try
                {
                    Waitings.WaitUntilPageIsOpen(_locator);
                    Logger.Info($"{_pagename} opened succesful");
                    return SingletonDriver.Source.FindElement(_locator).Displayed;
                }
                catch(System.Exception e) 
                {
                    Logger.Error($"Cannot open {_pagename}. {e.Message}");
                    return false;
                }
            } 
        }
    }
}
