using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using TechTalk.SpecFlow;
using QAWorks.Helpers;
using Newtonsoft.Json;

namespace REST_API
{
    // Represent the HTTP methods
    public enum HTTP_Verb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class API_HTTPClient
    {
        public Uri EndPoint { get; set; }
        public HTTP_Verb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        public API_HTTPClient()
        {
            EndPoint = new Uri("about:blank");
            Method = HTTP_Verb.GET;
            ContentType = "application/json";
            PostData = "";
        }

        public API_HTTPClient(Uri p_EndPoint, HTTP_Verb p_Method, string p_PostData, string p_ContentType = "text/json")
        {
            EndPoint = p_EndPoint;
            Method = p_Method;
            ContentType = p_ContentType;
            PostData = p_PostData;
        }

        // Make a request with parameters default the request parameters as an empty string
        // Places th response into the Scenario Context the response as a string
        public void PerformRequestResponse(string p_Parameters = "")
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + p_Parameters);
            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }
                }
                else
                    responseValue = String.Format("Faile: Received HTTP {0}", response.StatusCode);

                // Save the response data in the ScenarioContext object
                if (ScenarioContext.Current.ContainsKey("API_StatusCode"))
                    ScenarioContext.Current.Remove("API_StatusCode");
                ScenarioContext.Current.Add("API_StatusCode", response.StatusCode);

                if (ScenarioContext.Current.ContainsKey("API_Response"))
                    ScenarioContext.Current.Remove("API_Response");
                ScenarioContext.Current.Add("API_Response", responseValue);
            }
        }

        // Perform a post request with JSON string data object
        public void PostWithJson(string p_JSON)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint);
            request.ContentType = "application/json";
            request.Method = "POST";
            var js = Newtonsoft.Json.Linq.JObject.Parse(p_JSON);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                //string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(new
                //{

                //    userId = id,
                //    title = "Hello Title",
                //    body = "Hello World Hello Title"
                //});
                
                streamWriter.Write(js);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        // Perform a post with with a givent datatype
        public void PostWithDataType<T>(T p_DataType)
        {
            PostWithJson(HelperMethods.JsonSerializer<T>(p_DataType));
        }
    }
}   