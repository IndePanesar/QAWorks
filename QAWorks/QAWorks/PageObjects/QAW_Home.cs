using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Configuration;
using QAWorks.Helpers;
using OpenQA.Selenium.Support.PageObjects;

namespace QAWorks.PageObjects
{
    public class QAW_Home : QAW_NavigationBar 
    {
        #region Private variables
        private Uri _pageuri;

        #endregion

        #region Page Constructors
        public QAW_Home(IWebDriver driver) : base(driver)
        {
            _pageuri = new Uri(new Uri(ConfigurationManager.AppSettings["QAWorksURL"]), "/");
        }

        #endregion

        #region Private page methods and properties
        // Page web elements
        [FindsBy(How = How.CssSelector, Using = "div#AboutUsHeaderDesc h1")]
        private IWebElement _home_header { get; set; }
        #endregion

        #region Page Methods
        // Navigates to the About News page
        public void Open()
        {
            Driver.Navigate().GoToUrl(_pageuri.AbsoluteUri);
            HelperMethods.WaitForPageReady();
        }

        // Gets the current page URL as string
        public string GetPageURL()
        {
            return _pageuri.ToString();
        }

        // Returns the page title
        public override string Title()
        {
            return "Home Page - QAWorks";
        }

        // Returns the page header
        public override string Header()
        {
            return "Software Quality and Delivery Experts";
        }


        // Returns the text of the header tag
        public string GetPageHeaderText()
        {
            return _home_header.Text;
        }

        #endregion
    }
}
