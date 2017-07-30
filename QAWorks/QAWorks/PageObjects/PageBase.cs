// Author : I.S.Panesar 
// Date July 2017
// Description:
//          Base page object which all other pages will inherit
//

#region Usings
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

#endregion

namespace QAWorks.PageObjects
{
    public abstract class PageBase
    {
        #region Private variables
        protected IWebDriver Driver;

        #endregion

        #region Contructors
        public PageBase(IWebDriver p_Driver)
        {
            Driver = p_Driver;
            PageFactory.InitElements(Driver, this);
        }
        #endregion

        #region Public methods
        // Gets the current page title
        public abstract string Title();
        public abstract string Header();

        // Gets the webdriver URL for current page
        public string Url()
        {
            return Driver.Url;
        }
        #endregion
    }
}
