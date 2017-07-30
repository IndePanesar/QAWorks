// Author : I.S.Panesar
// Date July 2017
// Description:
//         Provides a single instance of requested Webdriver

using OpenQA.Selenium;
using System;
using System.Configuration;

namespace QAWorks.WebDriverCore
{
    // Singleton gets a single instance of the webdriver
    public static class DriverInstance
    {
        private static IWebDriver _driver;
        public static IWebDriver Driver
        {
            get
            {
                try
                {
                    if (null == _driver)
                    {
                        var browser = ConfigurationManager.AppSettings["Browser"];
                        _driver = WebDriverFactory.GetWebDriver((BrowserEnum)Enum.Parse(typeof(BrowserEnum), browser, true));
                    }
                    return _driver;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}
