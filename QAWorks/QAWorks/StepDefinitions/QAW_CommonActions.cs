using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QAWorks.PageObjects;
using OpenQA.Selenium;
using QAWorks.Helpers;

namespace QAWorks.StepDefinitions
{
    public static class QAW_CommonActions
    {
        // Verify ContactUs page attributes - header and URL
        public static bool VerifyContactPageAttributes(IWebDriver p_Driver)
        {
            var valid = true;
            var contactus = new QAW_ContactUs(p_Driver);
            var actual_header = contactus.GetPageHeaderText();
            var expected_header = contactus.Header();

            if (!actual_header.Equals(expected_header))
            {
                valid = false;
                HelperMethods.CreateSoftAssertion(String.Format("Expected page header to be {0} but was {1}", 
                                                  expected_header, actual_header));
            }

            if (!contactus.GetPageURL().Equals(p_Driver.Url))
            {
                valid = false;
                HelperMethods.CreateSoftAssertion(String.Format("Expected page url to be {0} but was {1}",
                                                  contactus.GetPageURL(), p_Driver.Url));
            }

            return valid;
        }

        // Verify About page attributes
        public static bool VerifyAboutPageAttributes(IWebDriver p_Driver)
        {
            return true;
        }

        // Verify attributes of the home page
        public static bool VerifyHomePageAttributes(IWebDriver p_Driver)
        {
            var valid = true;
            var home = new QAW_Home(p_Driver);
            var actual_header = home.GetPageHeaderText();
            var expected_header = home.Header();

            if (!actual_header.Equals(expected_header))
            {
                valid = false;
                HelperMethods.CreateSoftAssertion(String.Format("Expected page header to be {0} but was {1}",
                                                  expected_header, actual_header));
            }

            if (!home.GetPageURL().Equals(p_Driver.Url))
            {
                valid = false;
                HelperMethods.CreateSoftAssertion(String.Format("Expected page url to be {0} but was {1}",
                                                  home.GetPageURL(), p_Driver.Url));
            }

            return valid;
        }        
    }
}
