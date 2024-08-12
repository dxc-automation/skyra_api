using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.CodeAnalysis.Text;
using static demo.Data.FilePaths;

namespace demo.Data
{
    public class ReadData
    {
        public void ReadTestData()
        {
            FilePaths filePaths = new FilePaths();
            var jsonString = File.ReadAllText(filePaths.resourcesFile);
            JObject json = JObject.Parse(jsonString);

            Constants.baseUrl = json.SelectToken("$.Web.BaseURL").ToString();
            Constants.email   = Base64Decode(json.SelectToken("$.Web.Email").ToString());
            Constants.host    = json.SelectToken("$.API.Host").ToString();
        }


        public string ReadEnvironment()
        {
            string text = File.ReadAllText(envFile);
            Console.WriteLine(text);
            return text;
        }



        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}