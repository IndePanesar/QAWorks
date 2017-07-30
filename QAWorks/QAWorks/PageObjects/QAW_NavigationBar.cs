// Author : I.S.Panesar
// Date July 2017
// Description:
//          QAWorks page main navigation bar. This is displayed practically on all QAWorks pages.
//

#region Usings
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using QAWorks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using QAWorks.Helpers;
#endregion

namespace QAWorks.PageObjects
{
    public class QAW_NavigationBar : PageBase
    {
        // Enum representing the main navigation bar links
        public enum NavigationLinks_Enum
        {
            Home,
            Services,
            Works,
            Technology,
            Prices,
            News,
            About,
            Careers,
            Contact
        }

        #region Page Constructors
        // The only constructor for this that will take the driver object
        public QAW_NavigationBar(IWebDriver driver) : base(driver)
        {
        }

        #endregion

        #region Private page methods and properties
        // Page web elements
        [FindsBy(How = How.Id, Using = "logo")]
        private IWebElement _qaw_logo { get; set; }

        [FindsBy(How = How.XPath, Using = ".//ul[@id = \'menu\']//a[contains(text(),\'Home\')]")]
        private IWebElement _mnu_home { get; set; }

        [FindsBy(How = How.XPath, Using = ".//ul[@id = \'menu\']//a[contains(text(),\'Services\')]")]
        private IWebElement _mnu_services { get; set; }

        [FindsBy(How = How.XPath, Using = ".//ul[@id = \'menu\']//a[contains(text(),\'Works\')]")]
        private IWebElement _mnu_works { get; set; }

        [FindsBy(How = How.XPath, Using = ".//ul[@id = \'menu\']//a[contains(text(),\'Technology\')]")]
        private IWebElement _mnu_technology { get; set; }

        [FindsBy(How = How.XPath, Using = ".//ul[@id = \'menu\']//a[contains(text(),\'Prices\')]")]
        private IWebElement _mnu_prices { get; set; }

        [FindsBy(How = How.XPath, Using = ".//ul[@id = \'menu\']//a[contains(text(),\'News\')]")]
        private IWebElement _mnu_news { get; set; }

        [FindsBy(How = How.XPath, Using = ".//ul[@id = \'menu\']//a[contains(text(),\'About\')]")]
        private IWebElement _mnu_about { get; set; }

        [FindsBy(How = How.XPath, Using = ".//ul[@id = \'menu\']//a[contains(text(),\'Careers\')]")]
        private IWebElement _mnu_careers { get; set; }

        [FindsBy(How = How.XPath, Using = ".//ul[@id = \'menu\']//a[contains(text(),\'Contact\')]")]
        private IWebElement _mnu_contact { get; set; }

        // Function to return IWebElement for the navigation link
        private IWebElement GetMenuLinkElement(NavigationLinks_Enum p_eNavLink)
        {
            IWebElement element = null;
            switch (p_eNavLink)
            {
                case NavigationLinks_Enum.Home:
                    element = _mnu_home;
                    break;

                case NavigationLinks_Enum.Services:
                    element = _mnu_services;
                    break;

                case NavigationLinks_Enum.Works:
                    element = _mnu_works;
                    break;

                case NavigationLinks_Enum.Technology:
                    element = _mnu_technology;
                    break;

                case NavigationLinks_Enum.Prices:
                    element = _mnu_prices;
                    break;

                case NavigationLinks_Enum.News:
                    element = _mnu_news;
                    break;

                case NavigationLinks_Enum.About:
                    element = _mnu_about;
                    break;

                case NavigationLinks_Enum.Careers:
                    element = _mnu_careers;
                    break;

                case NavigationLinks_Enum.Contact:
                    element = _mnu_contact;
                    break;

                default:
                    // Should not end up here but ...
                    HelperMethods.CreateSoftAssertion(String.Format("Attempting to access a navigation link that is not handled yet {0}", p_eNavLink));
                    break;
            }

            return element;
        }

        #endregion

        #region Page public methods
        // Get the current page title
        public override string Title()
        {
            return Driver.Title;
        }

        // Returns header text - empty string
        public override string Header()
        {
            return String.Empty;
        }

        // QAWorks logo
        public Boolean QAWLogo_IsDisplayed()
        {
            HelperMethods.JavaScriptScrollToElement(_qaw_logo);
            return _qaw_logo.Displayed;
        }

        // Function returns navigation element is displayed
        public bool NavigationLink_IsDisplayed(NavigationLinks_Enum p_eNavLink)
        {
            var element = GetMenuLinkElement(p_eNavLink);
            if (element == null)
                return false;

            HelperMethods.JavaScriptScrollToElement(element);
            return element.Displayed;
        }

        // Function to click navigation link
        public void NavigationLink_Click(NavigationLinks_Enum p_eNavLink)
        {
            var element = GetMenuLinkElement(p_eNavLink);
            if (element == null)
                return;

            HelperMethods.JavaScriptScrollToElement(element);
            element.Click();
        }

        #endregion
    }
}
