using mailslurp.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demo.Data;
using demo.Config;

namespace demo.Utility
{
    public class MailSurp
    {
        private static string baseUrl = "https://api.mailslurp.com";
        private static string apiKey = "bdc764f95c3982fc5a5bfb4196014f6314fbe8734a5fbaf3f6d90b7b26b7180d";


        public static void CreateInbox()
        {
            string url = baseUrl + "/createInbox";

            var client = RestHelper.GetClient();
            var request = new RestRequest(url, Method.Post);

            // Optional headers
            request.AddQueryParameter("allowTeamAccess", "");
            request.AddQueryParameter("useDomainPool", "");
            request.AddQueryParameter("expiresAt", "");
            request.AddQueryParameter("expiresIn", "");
            request.AddQueryParameter("emailAddress", "");
            request.AddQueryParameter("inboxType", "");
            request.AddQueryParameter("description", "");
            request.AddQueryParameter("name", "");
            request.AddQueryParameter("tags", "");
            request.AddQueryParameter("tags", "");
            request.AddQueryParameter("favourite", "");

            // Required headers
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("api-key", "49d4b49f568715896d35e3757602dc9ba1c0bc33fcd368862ef038bddcc34831");
            request.AddHeader("x-api-key", apiKey);

            // Send request
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            Logger.GetReportDetails(response);

            // Parse JSON response body 
            var json = RestHelper.ParseResponse(response.Content);
            Console.WriteLine("\n" + json);

            if (json != null)
            {
                Constants.emailAddress = json.SelectToken("$..emailAddress").ToString();
                Constants.inboxId = json.SelectToken("$..id").ToString();
                Console.WriteLine("\n" + Constants.emailAddress + "\n" + Constants.inboxId);
            }
        }

        public static void DeleteAllInboxes()
        {
            string url = baseUrl + "/inboxes";

            var client = RestHelper.GetClient();
            var request = new RestRequest(url, Method.Delete);

            // Required headers
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("api-key", "49d4b49f568715896d35e3757602dc9ba1c0bc33fcd368862ef038bddcc34831");
            request.AddHeader("x-api-key", apiKey);

            // Send request
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            Logger.GetReportDetails(response);

            // Parse JSON response body 
            var json = RestHelper.ParseResponse(response.Content);
            Console.WriteLine("\n" + json);
        }


        public static void GetInboxContent(string inboxId)
        {
            string url = baseUrl + "/inboxes/" + inboxId + "/emails/paginated";

            var client = RestHelper.GetClient();
            var request = new RestRequest(url, Method.Get);

            // Optional
            request.AddQueryParameter("sort", "ASC");

            // Required headers
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("api-key", "49d4b49f568715896d35e3757602dc9ba1c0bc33fcd368862ef038bddcc34831");
            request.AddHeader("x-api-key", apiKey);

            // Send request
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            Logger.GetReportDetails(response);

            // Parse JSON response body 
            var json = RestHelper.ParseResponse(response.Content);
            Console.WriteLine("\n" + json);
            Constants.emailId = RestHelper.GetJValue(response, "$..id");
            Console.WriteLine("\nSelected mail id from inbox is " + Constants.emailId);
        }


        public static void GetEmailContent(string emailId)
        {
            string url = baseUrl + "/emails/" + emailId + "/links";

            var client = RestHelper.GetClient();
            var request = new RestRequest(url, Method.Get);

            // Required headers
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("api-key", apiKey);
            request.AddHeader("x-api-key", apiKey);

            // Send request
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            Logger.GetReportDetails(response);

            // Parse JSON response body 
            var json = RestHelper.ParseResponse(response.Content);
            Console.WriteLine("\n" + json);

            var links = json.SelectToken("$..links");
            Console.WriteLine("\nSelected links are " + links);

            foreach (string link in links)
            {
                if (link.Contains("user"))
                {
                    Constants.invitationLink = link;
                    Console.WriteLine("\nInvitation link " + Constants.invitationLink);
                }
            }
        }
    }
}
