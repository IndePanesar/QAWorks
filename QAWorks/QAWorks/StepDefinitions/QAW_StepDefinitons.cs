// Author : I.S.Panesar
// Date July 2017
// Description:
//          Step definitions for the features
//

using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using QAWorks.Helpers;
using QAWorks.PageObjects;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace QAWorks.StepDefinitions
{
    [Binding]
    public class QAW_StepDefinitons
    {
        #region Private variables
        private QAW_Home _home;
        private QAW_ContactUs _contactus;

        private static IWebDriver _driver;
        #endregion

        #region Hooks
        [BeforeScenario("FunctionalTests")]
        public void BeforeScenario()
        {
            _driver = WebDriverCore.DriverInstance.Driver;
        }
        
        [AfterScenario("FunctionalTests")]
        public void AfterScenario()
        {
            // An error has occurred have take a screen shot and shut browser
            var error = ScenarioContext.Current.TestError;
            if (null != error)
            {
                var test = ScenarioContext.Current.ScenarioInfo.Title;
                HelperMethods.CreateSoftAssertion(String.Format("Unhandled error in {0} during test. Error info -\n{1} \n{2}",
                                                                 test, error.Message, error.GetType().Name));
                // Close browser
                if (null != _driver)
                {
                    _driver.Quit();
                    _driver = null;
                }
            }

            // Verify the soft assertions
            HelperMethods.VerifySoftAssertions();                
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            if (null != _driver)
            {
                _driver.Quit();
                _driver = null;
            }
        }

        #endregion

        #region Tests
        [Given(@"I am on the QAWorks web site")]
        public void GivenIAmOnTheQAWorksWebSite()
        {
            _home = new QAW_Home(_driver);
            _home.Open();
        }

        [When(@"I click on the ""(.*)"" main menu item")]
        public void WhenIClickOnTheMainMenuItem(string p_MenuItem)
        {
            var navlink = QAW_NavigationBar.NavigationLinks_Enum.Home;
            switch (p_MenuItem.Trim().ToLower())
            {
                case "home":
                    navlink = QAW_NavigationBar.NavigationLinks_Enum.Home;
                    break;
                case "services":
                    navlink = QAW_NavigationBar.NavigationLinks_Enum.Services;
                    break;
                case "works":
                    navlink = QAW_NavigationBar.NavigationLinks_Enum.Works;
                    break;
                case "technology":
                    navlink = QAW_NavigationBar.NavigationLinks_Enum.Technology;
                    break;
                case "prices":
                    navlink = QAW_NavigationBar.NavigationLinks_Enum.Prices;
                    break;
                case "news":
                    navlink = QAW_NavigationBar.NavigationLinks_Enum.News;
                    break;
                case "about":
                    navlink = QAW_NavigationBar.NavigationLinks_Enum.About;
                    break;
                case "careers":
                    navlink = QAW_NavigationBar.NavigationLinks_Enum.Careers;
                    break;
                case "contactus":
                    navlink = QAW_NavigationBar.NavigationLinks_Enum.Contact;
                    break;
                default:
                    Assert.Fail(String.Format("Menu item {0} is not handled", p_MenuItem));
                    return;
            }
            _home = _home ?? new QAW_Home(_driver);
            _home.NavigationLink_Click(navlink);
        }

        [Then(@"the ""(.*)"" page is redisplayed")]
        [Then(@"I land on the ""(.*)"" page")]
        public void ThenILandOnThePage(string p_Page)
        {
            var isvalid = false;

            switch(p_Page.Trim().ToLower())
            {
                case "contactus":
                    isvalid = QAW_CommonActions.VerifyContactPageAttributes(_driver);
                    break;
                case "about":
                    isvalid = QAW_CommonActions.VerifyAboutPageAttributes(_driver);
                    break;
                case "home":
                    isvalid = QAW_CommonActions.VerifyHomePageAttributes(_driver);
                    break;
                default:
                    HelperMethods.CreateSoftAssertion(String.Format("QAWorks page type {0} is not yet handled", p_Page));
                    break;
            }
        }

        [Then(@"I should be able to contact QAWorks with the following information")]
        public void ThenIShouldBeAbleToContactQAWorksWithTheFollowingInformation(Table p_Table)
        {
            var cls_contactus = p_Table.CreateInstance<QAW_clsContactUs>();
            _contactus = _contactus ?? new QAW_ContactUs(_driver);

            // Fill in the form with the specified data
            _contactus.EnterName(cls_contactus.Name);
            _contactus.EnterMessage(cls_contactus.Message);
            _contactus.EnterEmail(cls_contactus.Email);

            _contactus.ClickSend();
        }

        [When(@"I populate the ContactUs form with data ""(.*)""")]
        public void WhenIPopulateTheContactUsFormWithData(string p_NameEmailMessage)
        {
            // The data is seperated by a "~"
            var data = p_NameEmailMessage.Split('~');
            _contactus = _contactus ?? new QAW_ContactUs(_driver);
            if (!String.IsNullOrEmpty(data[0]))
                _contactus.EnterName(data[0], true);
            if (!String.IsNullOrEmpty(data[1]))
                _contactus.EnterEmail(data[1], true);
            if (!String.IsNullOrEmpty(data[2]))
                _contactus.EnterMessage(data[2], true);
        }

        [Then(@"I should see error text (.*)")]
        public void ThenIShouldSeeErrorText(string p_ErrorTexts)
        {
            // The data is seperated by a "~"
            var expectederrors = p_ErrorTexts.Split('~').ToList<string>();

            _contactus = _contactus ?? new QAW_ContactUs(_driver);
            var actualerrors = _contactus.GetContactUsErrors();

            // Check the error counts are the same
            if (actualerrors.Count != expectederrors.Count)
            {                 
                var sbact = new System.Text.StringBuilder();
                sbact.Append(String.Format("Expected error count {0} but is {1}\n", expectederrors.Count, actualerrors.Count));
                sbact.Append("Expected errors:\n");
                foreach (var errs in expectederrors)
                {
                    sbact.Append("\t" + errs.ToString() + "\n");
                }

                sbact.Append("Actual errors:\n");
                foreach (var errs in actualerrors)
                {
                    sbact.Append("\t" + errs.ToString() + "\n");
                }
                HelperMethods.CreateSoftAssertion(sbact.ToString());
            }

            // Check the actual error texts match the expected
            var actualdifferrs = actualerrors.Except<string>(expectederrors).ToList<string>();
            var expectedifferrs = expectederrors.Except<string>(actualerrors).ToList<string>();

            if (actualdifferrs.Count > 0 || expectedifferrs.Count > 0)
            {
                var sbact = new System.Text.StringBuilder();
                sbact.Append("Expected errors:\n");
                foreach (var errs in expectedifferrs)
                {
                    sbact.Append("\t" + errs.ToString() + "\n");
                }

                sbact.Append("Actual errors:\n");
                foreach (var errs in actualdifferrs)
                {
                    sbact.Append("\t" + errs.ToString() + "\n");
                }

                HelperMethods.CreateSoftAssertion(sbact.ToString());
            }
        }

        #endregion
    }
}
