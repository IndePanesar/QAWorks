// Author : I.S.Panesar
// Date July 2017
// Description:
//          Helper methods
//

#region Usings
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

#endregion

namespace QAWorks.Helpers
{
    public static class HelperMethods
    {
        #region Private variables
        private static IWebDriver _driver = WebDriverCore.DriverInstance.Driver;
        private static List<string> _softassertlist = null;
        
        #endregion
        
        #region Soft Assertion
        // Add the error message to the assertions list
        public static void CreateSoftAssertion(string p_AssertMsg)
        {
            if (null == _softassertlist)
                _softassertlist = new List<String>();

            if (!String.IsNullOrEmpty(p_AssertMsg))
                _softassertlist.Add(p_AssertMsg);
        }

        // Verify the assertions and clear the soft assertions list down
        public static void VerifySoftAssertions()
        {
            if (null == _softassertlist || 0 == _softassertlist.Count)
                return;
            int acount = _softassertlist.Count();
            Console.WriteLine("\n============= Soft Assertion Error List Start =============\n");
            Console.WriteLine(String.Format("Soft Assertion Error List contains {0} error{1}:\n", acount , acount > 1 ? "s" : ""));
            acount = 1;
            foreach(string saitem in _softassertlist)
            {
                Console.WriteLine(String.Format("{0}. {1}", acount++, saitem));
            }
            Console.WriteLine("============= Soft Assertion Error List End =============\n");
            _softassertlist.Clear();
            Assert.Fail("Soft Assertions were encountered during test runs. Please refer to the test output/report for further details");
        }

        // Function returns the path of the bin folder for the currently executing assembly
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        // Function to enter text into an input text field
        public static void EnterElementText(IWebElement p_Element, string p_Value, bool p_Tab)
        {
            HelperMethods.JavaScriptScrollToElement(p_Element);
            p_Element.Clear();
            p_Element.SendKeys(p_Value);
            if (p_Tab)
                p_Element.SendKeys(Keys.Tab);
        }

        # region Use Javscript Methods
        public static void JavaScriptScrollToElement(IWebElement p_Element)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", p_Element);
            Thread.Sleep(1000);
        }

        public static void WaitForPageReady()
        {
            //Wait upto 30 seconds for page load
            for (int attempts = 0; attempts < 60; attempts++)
            { 
                try
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    //To check page ready state.
                    if (((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").ToString().ToLower().Equals("complete"))
                        break;                    
                }
                catch(Exception exp)
                {
                } 
            }
        }
        #endregion

        #region JSON serialisation and deserialisation
        // Function to serialize a data type to JSON string 
        public static string JsonSerializer<T>(T p_T)
        {
            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            dcjs.WriteObject(ms, p_T);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T objT = (T)ser.ReadObject(ms);
            return objT;
        }
        #endregion
    }
    #endregion
}