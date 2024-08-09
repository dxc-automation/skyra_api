using RestSharp;
using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Interfaces;
using demo.Utility;
using demo.Data;
using demo.Config;

namespace demo.TestScripts.API
{
    public class UserInvitations_List
    {

        public void GET_METHOD(string token, int expectedStatusCode)
        {
            // Change the endpoint path
            string url = Constants.baseUrl;

            var client = RestHelper.GetClient();
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Authorization", "Bearer " + token);
            Constants.requestContent = Logger.GetRequestDetails(request);

            // Send request
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            Logger.GetReportDetails(response);

            int actualStatusCode = (int)response.StatusCode;
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "Received status code is incorrect.");
        }


        public void POST_METHOD(string token, int expectedStatusCode)
        {
            // Change the endpoint path
            string url = Constants.host + "users";


            // This row is your main JSON object in the request body
            Constants.requestBody = new JObject();
            Constants.requestBody.Add("FIELD", "VALUE");


            var client = RestHelper.GetClient();
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddStringBody(Constants.requestBody.ToString(), DataFormat.Json);
            Constants.requestContent = Logger.GetRequestDetails(request);


            // Send request
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            Logger.GetReportDetails(response);


            // Parsing response body
            var json = RestHelper.ParseResponse(response.Content);


            int actualStatusCode = (int)response.StatusCode;
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "Received status code is incorrect.");
        }


        public void PATCH_METHOD(string token, int expectedStatusCode)
        {
            // Change the endpoint path
            string url = Constants.host + "users";


            // This row is your main JSON object in the request body
            Constants.requestBody = new JObject();
            Constants.requestBody.Add("FIELD", "VALUE");


            var client = RestHelper.GetClient();
            var request = new RestRequest(url, Method.Patch);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddStringBody(Constants.requestBody.ToString(), DataFormat.Json);
            Constants.requestContent = Logger.GetRequestDetails(request);


            // Send request
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            Logger.GetReportDetails(response);


            // Parsing response body
            var json = RestHelper.ParseResponse(response.Content);

            // Get specific value
            string value = RestHelper.GetJValue(response, "$..");
            Console.WriteLine("\nGenerated Invitation ID " + value);

            int actualStatusCode = (int)response.StatusCode;
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "Received status code is incorrect.");
        }
    }
}

