using NUnit.Framework;
using REST_API;
using System;
using TechTalk.SpecFlow;

namespace EqualExperts.StepDefinitions
{
    [Binding]
    class QAW_APIStepDefinitions
    {
        #region API tests
        [Given(@"I have an endpoint '(.*)'")]
        public void GivenIHaveAnEndpoint(string p_URL)
        {
            var url = (p_URL.StartsWith("http")) ? p_URL : "http://" + p_URL;

            if (ScenarioContext.Current.ContainsKey("API_uri"))
                ScenarioContext.Current.Remove("API_uri");

            ScenarioContext.Current.Add("API_uri", new Uri(url));
        }

        [When(@"I submit http request '(.*)' with '(.*)'")]
        public void WhenISubmitHttpRequestWith(string p_Request, string p_Data)
        {
            var postwithdata = false;
            var uri = (Uri)ScenarioContext.Current["API_uri"];
            var apiclient = new API_HTTPClient();
            apiclient.EndPoint = (Uri)ScenarioContext.Current["API_uri"];
            var method = (HTTP_Verb)Enum.Parse(typeof(HTTP_Verb), p_Request, true);
            apiclient.Method = method;

            switch (method)
            {
                case HTTP_Verb.GET:
                    apiclient.Method = method; ;
                    break;
                case HTTP_Verb.POST:
                    postwithdata = true;
                    apiclient.PostData = p_Data;
                    break;
                case HTTP_Verb.PUT:
                    break;
                case HTTP_Verb.DELETE:
                    break;
                default:
                    Assert.Fail("");
                    break;
            }

            if (postwithdata)
                apiclient.PostWithJson(p_Data);
            else
                apiclient.PerformRequestResponse();
        }

        [Then(@"I should see response code of '(.*)'")]
        public void ThenIShouldSeeResponseCodeOf(string p_ExpectedStatus)
        {
            if (ScenarioContext.Current.ContainsKey("API_StatusCode"))
            {
                var actualstatus = (System.Net.HttpStatusCode)ScenarioContext.Current["API_StatusCode"];
                Assert.IsTrue(actualstatus == (System.Net.HttpStatusCode)Enum.Parse(typeof(System.Net.HttpStatusCode), p_ExpectedStatus, true),
                              string.Format("Http Status expected {0} but was {1}", p_ExpectedStatus, actualstatus));
            }
        }

        [Then(@"I should see JSON response '(.*)'")]
        public void ThenIShouldSeeJSONResponse(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        #endregion

    }
}
