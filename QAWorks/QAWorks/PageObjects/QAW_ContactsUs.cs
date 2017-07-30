// Author : I.S.Panesar
// Date July 2017
// Description:
//          QAWorks ContactUs page object
//

#region Usings
using QAWorks.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
#endregion

namespace QAWorks.PageObjects
{
    public class QAW_ContactUs : QAW_NavigationBar
    {
        #region Private variables
        private Uri _pageuri;

        #endregion

        #region Page Constructors

        public QAW_ContactUs(IWebDriver driver) : base(driver)
        {
            _pageuri = new Uri(new Uri(ConfigurationManager.AppSettings["QAWorksURL"]), "contact.aspx");
        }

        #endregion

        #region Private page methods and properties
        // Page web elements
        [FindsBy(How = How.CssSelector, Using = "div#ContactHead h1")]
        private IWebElement _contactus_header { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a[title = \'Email us!\']")]
        private IWebElement _contactus_emailus_link { get; set; }

        [FindsBy(How = How.XPath, Using = ".//a[contains(text, \'current vacancies\')]")]
        private IWebElement _contactus_currentvacancies_link { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_NameBox")]
        private IWebElement _contactus_nameinput { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_rfvName")]
        private IWebElement _contactus_errreqname { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_MessageBox")]
        private IWebElement _contactus_messageinput { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_rfvMessage")]
        private IWebElement _contactus_errreqmessage { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_EmailBox")]
        private IWebElement _contactus_emailinput { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_rfvEmailAddress")]
        private IWebElement _contactus_errreqemail { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_revEmailAddress")]
        private IWebElement _contactus_errinvemail { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_SendButton")]
        private IWebElement _contactus_sendbutton { get; set; }
        
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

        // Get the current page title
        public override string Title()
        {
            return "Contact Us - QAWorks";
        }

        // Returns the page header
        public override string Header()
        {
            return "Contact";
        }

        // Returns the text of the header tag
        public string GetPageHeaderText()
        {
            return _contactus_header.Text;
        }

        // Enter name
        public void EnterName(string p_Name, bool p_Tab = false)
        {
            HelperMethods.EnterElementText(_contactus_nameinput, p_Name, p_Tab);
        }

        // Enter message
        public void EnterMessage(string p_Message, bool p_Tab = false)
        {
            HelperMethods.EnterElementText(_contactus_messageinput, p_Message, p_Tab);
        }

        // Enter email
        public void EnterEmail(string p_Email, bool p_Tab = false)
        {
            HelperMethods.EnterElementText(_contactus_emailinput, p_Email, p_Tab);
        }

        // Click submit button
        public void ClickSend()
        {
            _contactus_sendbutton.Click();
        }

        // Returns list of error messages displayed
        public List<string> GetContactUsErrors()
        {
            var errlist = new List<string>();
            
            var errelements = Driver.FindElements(By.CssSelector("span[id ^='ctl00_MainContent_rfv']"));
            foreach (var el in errelements)
                if (!String.IsNullOrWhiteSpace(el.Text))
                    errlist.Add(el.Text);

            errelements = Driver.FindElements(By.CssSelector("span[id ^='ctl00_MainContent_rev']"));
            foreach (var el in errelements)
                if (!String.IsNullOrWhiteSpace(el.Text))
                    errlist.Add(el.Text);

            return errlist;
        }
        #endregion
    }
}
