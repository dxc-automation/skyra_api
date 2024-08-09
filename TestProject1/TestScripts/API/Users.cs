using RestSharp;
using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Interfaces;
using demo.Utility;
using demo.Data;
using demo.Config;

namespace demo.TestScripts.API
{
    public class Users
    {

        public string GetUsers(int expectedStatusCode, string expression)
        {
            // Change the endpoint path
            string url = Constants.host + "users";

            var client = RestHelper.GetClient();
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept", "*/*");
            Constants.requestContent = Logger.GetRequestDetails(request);

            // Send request
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            Logger.GetReportDetails(response);

            var json = RestHelper.ParseResponse(response.Content);
            string value = RestHelper.GetJValue(response, expression);

            int actualStatusCode = (int)response.StatusCode;
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "Received status code is incorrect.");

            return value;
        }
    }
}

