using Newtonsoft.Json.Linq;
using RestSharp;
using System;


namespace demo.Config
{
    public class RestHelper
    {

        private static RestClient? client;
        public static RestResponse? response { get; set; }
        public static RestRequest request = new RestRequest();


        public static RestClient GetClient()
        {
            var options = new RestClientOptions
            {
                FailOnDeserializationError = false,
                ThrowOnAnyError = false
            };
            client = new RestClient(options);
            return client;
        }


        public static JObject ParseResponse(string responseContent)
        {
            JObject content = new JObject();
            try
            {
                content = JObject.Parse(responseContent);
                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine(content);
            }
            return content;
        }


        public static string GetJValue(RestResponse response, string key)
        {
            string? value;
            try
            {
                var json = JObject.Parse(response.Content);
                Console.WriteLine("\nSearching in : \n" + json);

                value = json.SelectToken(key).ToString();
                Console.WriteLine("\nSearching for : \n" + key);
                Console.WriteLine("\nResult is : \n" + value);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nReading JSON FAILED\n" + response.Content);
                value = response.ErrorMessage;
            }
            return value;
        }


        public static string GetValue(string content, string key)
        {
            var json = JObject.Parse(content);
            Console.WriteLine("\nSearching in : \n" + json);
            string? value;
            try
            {
                value = json.SelectToken(key).ToString();
                Console.WriteLine("\nSearching for : \n" + key);
                Console.WriteLine("\nResult is : \n" + value);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nReading JSON FAILED\n" + response.Content);
                value = response.ErrorMessage;
            }
            return value;
        }

    }
}
