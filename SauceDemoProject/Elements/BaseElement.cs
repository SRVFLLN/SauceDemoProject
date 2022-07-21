using OpenQA.Selenium;
using System;
using System.Collections.Generic;

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
            try
            {
                Waitings.WaitForElementIsDisplayed(this);
                return SingletonDriver.Source.FindElement(_locator);
            }
            catch
            {
                Logger.Error($"Element with locator {_locator} not found!");
                throw;
            }
        }

        public void Click(int countOfAttempts, params Exception[] exceptionsForHandle)
        {
            try
            {
                Waitings.WaitForElementIsDisplayed(this);
                List<Exception> handledException = null;
                if (exceptionsForHandle != null) 
                {
                    handledException = new List<Exception>();
                    handledException.AddRange(exceptionsForHandle); 
                }
                Logger.Info($"Click on {_name} element...");
                while (countOfAttempts > 0)
                {
                    try
                    {
                        FindElement().Click();
                        countOfAttempts--;
                    }
                    catch (Exception ex)
                    {
                        if (handledException.Contains(ex))
                        {
                            countOfAttempts--;
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Logger.Error($"Element with locator {_locator} is not clicable! {e.Message}");
                throw;
            }
        }

        public void Click(Func<IWebDriver, bool> condition = null) 
        {
            try
            {
                Waitings.WaitForElementIsDisplayed(this);
                FindElement().Click();
                Waitings.WaitUntilCondition(condition);
                return;
            }
            catch
            {
                return;
            }
        }

        public void Click() 
        {
            try
            {
                Waitings.WaitForElementIsDisplayed(this);
                FindElement().Click();
            }
            catch(Exception e)
            {
                Logger.Error($"Element with locator {_locator} is not clicable! {e.Message}");
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
    }
}