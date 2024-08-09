using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RestSharp;
using System;
using System.Text;
using System.Threading;
using NUnit.Framework;
using static demo.Config.TestBase;
using static demo.Data.FilePaths;
using static demo.Utility.Utils;
using System.Diagnostics;
using System.Net.NetworkInformation;
using mailslurp.Model;
using demo.Data;

namespace demo.Config
{
    public class Logger
    {
        public static ExtentReports extent;
        public static ExtentTest test;
        private static ExtentSparkReporter htmlReporter;

        public static string? reportFileName { get; set; }
        public static string? reportZipName { get; set; }


        //***   Used for formatting json
        public static string PrettyJson(string responseContent)
        {
            string body;
            try
            {

                return body = JToken.Parse(responseContent).ToString(Formatting.Indented);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return body = ex.Message;
            }
        }


        public static ExtentReports GetExtent()
        {
            string time = DateTime.Now.ToString("HH-mm-ss");
            var testCaseID = NUnit.Framework.TestContext.CurrentContext.Test.Properties.Get("TestCaseID");
            var category = NUnit.Framework.TestContext.CurrentContext.Test.Properties.Get("Category");
            reportFileName = category + "_" + testCaseID + "_" + time;
            reportZipName = category + "_" + time;

            string reportFile = Path.Combine(projectRootPath, @"Report\" + reportFileName + ".html");

            htmlReporter = new ExtentSparkReporter(reportFile);
            htmlReporter.Config.CSS = ".report-name { padding-left:85px; }" +
                                      ".report-name > img { float:left; width: auto; position: fixed; }" +
                                      ".badge-primary { background-color: white; color: black }" +
                                      ".side-nav { background-color: white }" +
                                      ".header-dark.header { background-color: white; }" +
                                      ".opacity-01 { opacity: 100; }";

            htmlReporter.Config.DocumentTitle = "Automation Test Report";
            htmlReporter.Config.TimelineEnabled = true;
            var file = Path.Combine(dir + @"\\Resources\\logo.png");
            htmlReporter.Config.ReportName = "<img src='" + file + "' />";

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo(Environment.MachineName, "Machine Name");
            extent.AddSystemInfo(Environment.OSVersion.Platform.ToString(), "OS");
            extent.AddSystemInfo(Environment.OSVersion.ServicePack.ToString(), "OS");
            extent.AddSystemInfo(Environment.UserName, "User");
            extent.AddSystemInfo(Environment.NewLine, "");

            return extent;
        }



        public static void StartTestReport(string testDescription)
        {
            var testName = NUnit.Framework.TestContext.CurrentContext.Test.Name;

            test = extent.CreateTest(
                "<b>" + testName + "</b>",
                "<pre><center>"
                        + "<center><b>* * * * * * * *    I N F O R M A T I O N    * * * * * * * *</b></center>"
                        + "<p align=justify><br><br>"
                        + testDescription
                        + "</p>"
                        + "</pre>");
        }


        public static void EndTestReport()
        {
            var status = NUnit.Framework.TestContext.CurrentContext.Result.Outcome.Status;
            var testName = NUnit.Framework.TestContext.CurrentContext.Test.Name;
            var testCaseID = NUnit.Framework.TestContext.CurrentContext.Test.Properties.Get("TestCaseID");
            var category = NUnit.Framework.TestContext.CurrentContext.Test.Properties.Get("Category");
            Console.WriteLine("\n" + testName + " " + testCaseID + " - " + status);


            if (testName.Contains("WEB"))
            {
                string msg = NUnit.Framework.TestContext.CurrentContext.Result.Message;
                var dateTime = DateTime.Now.ToString("yyyy_MM_dd").ToString();
                var media = Capture(_browserDriver, dateTime);

                switch (status)
                {
                    case TestStatus.Failed:
                        test.Log(Status.Fail, msg, MediaEntityBuilder.CreateScreenCaptureFromPath(media.ToString()).Build());
                        break;

                    case TestStatus.Passed:
                        test.Log(Status.Pass, MediaEntityBuilder.CreateScreenCaptureFromPath(media.ToString()).Build());
                        break;
                }
            }
            else
            {
                switch (status)
                {
                    //***   FAILED
                    case TestStatus.Failed:

                        string msg = NUnit.Framework.TestContext.CurrentContext.Result.Message;

                        test.Fail("<pre><b>REQUEST PARAMETERS</b>"
                             + "<br/><br/>"
                             + "Method " + Constants.requestMethod
                             + "<br>"
                             + Constants.requestResource
                             + "<br/><br/>"
                             + Constants.requestContent
                             + "<br/><br/></pre><br>"
                             + "<pre><b>RESPONSE PARAMETERS </b>"
                             + "<br/><br/>"
                             + "Response Code &#x09;" + Constants.responseStatusCode
                             + "<br/>"
                              + "Status &#x09;" + Constants.responseStatus
                             + "<br/><br/>"
                             + Constants.responseErrorMsg
                             + "<br/><br/>"
                             + "Error  " + msg
                             + "<br/><br/>"
                             + PrettyJson(Constants.responseContent)
                             + "<br/>"
                             + "</pre>");
                        break;

                    //***   PASSED
                    case TestStatus.Passed:

                        test.Pass("<pre><b>REQUEST PARAMETERS</b>"
                             + "<br/><br/>"
                             + "Method " + Constants.requestMethod
                             + "<br>"
                             + Constants.requestResource
                             + "<br/><br/>"
                             + Constants.requestContent
                             + "<br/><br/></pre><br>"
                             + "<pre><b>RESPONSE PARAMETERS </b>"
                             + "<br/><br/>"
                             + "Response Code &#x09;" + Constants.responseStatusCode
                             + "<br/>"
                             + "Status &#x09;" + Constants.responseStatus
                             + "<br/><br/>"
                             + PrettyJson(Constants.responseContent)
                             + "<br/>"
                             + "</pre>");
                        break;


                    case TestStatus.Skipped:
                        test.Log(Status.Skip, "Test ended with " + Status.Skip);
                        break;
                }
            }
            PrintReport();
        }


        public static void GetReportDetails(RestResponse response)
        {
            Constants.responseContent = response.Content;
            Constants.responseStatus = response.ResponseStatus.ToString();
            Constants.responseStatusCode = (int?)response.StatusCode;
            Constants.requestMethod = response.Request.Method.ToString().ToUpper();
            Constants.requestResource = response.Request.Resource.ToString();
            Constants.responseErrorMsg = response.ErrorMessage;
        }


        // Get rest request parameters
        public static string GetRequestDetails(RestRequest request)
        {
            var sb = new StringBuilder();
            foreach (var param in request.Parameters)
            {
                sb.AppendFormat("\n{0}{1}\r\n", param.Name, param.Value);
            }
            return sb.ToString();
        }


        // Generating HTML file
        public static void PrintReport()
        {
            Console.WriteLine("\nGenerating test report...........");
            //End Report

            extent.Flush();
        }



        private static Uri Capture(IWebDriver driver, string screenShotName)
        {
            string localpath = "";
            Uri relative = null;

            try
            {
                Thread.Sleep(4000);
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();

                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");

                DirectoryInfo di = Directory.CreateDirectory(dir + "\\Report\\Screenshots\\");
                string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "\\Report\\Screenshots\\" + screenShotName + ".png";

                localpath = new Uri(finalpth).LocalPath;
                relative = new Uri(finalpth);
                screenshot.SaveAsFile(localpath);
            }
            catch (Exception e)
            {
                throw e;
            }

            return relative;
        }
    }
}


