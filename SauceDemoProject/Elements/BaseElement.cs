using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SauceDemoProject.Elements
{
    public abstract class BaseElement 
    {
        private readonly By _locator;
        private readonly string _name;
        public string Name => _name;
        public By Locator => _locator;

        public BaseElement(By locator, string name)
        {
            _locator = locator;
            _name = name;
        }

        protected IWebElement FindElement()
        {
            //try
            //{
                Waitings.WaitForElementIsDisplayed(this);
                return SingletonDriver.Source.FindElement(_locator);
            //}
            //catch 
            //{
            //    Logger.Error($"Element with locator {_locator} not found!");
            //    throw;
            //}
        }

        protected IList<IWebElement> FindElements() => SingletonDriver.Source.FindElements(_locator);

        public void Click(int countOfTryes = 0, params Exception[] handledException)
        {
            try
            {
                Waitings.WaitForElementIsDisplayed(this);
                Logger.Info($"Click on {_name} element...");
                if (countOfTryes != 0) 
                {
                    while (countOfTryes > 0) 
                    {
                        try
                        {
                            FindElement().Click();
                            countOfTryes--;
                        }
                        catch(Exception ex)
                        {
                            if (handledException.Contains(ex))
                            {
                                countOfTryes--;
                            }
                            else 
                            {
                                throw;
                            }
                        }
                    }
                }
                FindElement().Click();
            }
            catch(Exception e)
            {
                Logger.Error($"Element with locator {_locator} is not clicable! {e.Message}");
                throw;
            }
        }

        public bool IsDisplayed 
        {
            get 
            {
                try 
                {
                    Waitings.WaitForElementIsDisplayed(this);
                    return FindElement().Displayed;
                }
                catch
                {
                    return false;
                }
            }
        }

        public string Text => FindElement().Text;

        public string GetAttribute(string attribute)
        {
            try
            {
                Logger.Info($"Getting attribute {attribute} from {Name}...");
                return FindElement().GetAttribute($"{attribute}");
            }
            catch(Exception e)
            {
                Logger.Error($"{_name} don't have {attribute} attribute!",e.Message);
                throw;
            }
        }

        public IWebElement GetElement() 
        {
            return FindElement();
        }
    }
}