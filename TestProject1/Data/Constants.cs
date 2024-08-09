using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Data
{
    public class Constants
    {
        //***   Tests
        public static string? baseUrl { get; set; }
        public static string? email { get; set; }
        public static string? host { get; set; }
        public static string? testCategory { get; set; }
        public static string? environment { get; set; }
        public static string? userID { get; set; }


        //***   Response
        public static string? requestContent { get; set; }
        public static string? responseContent { get; set; }
        public static JObject? requestBody { get; set; }
        public static int? responseStatusCode { get; set; }
        public static string? responseStatus { get; set; }
        public static string? requestMethod { get; set; }
        public static string? requestResource { get; set; }
        public static string? responseErrorMsg { get; set; }



        //***   MailSurp
        public static string? emailAddress { get; set; }
        public static string? inboxId { get; set; }
        public static string? emailId { get; set; }
        public static string? invitationLink { get; set; }
    }
}
