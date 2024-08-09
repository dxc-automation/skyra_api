using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO.Compression;
using static demo.Drivers.DesktopDriverManager;
using static demo.Drivers.BrowserDriverManager;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using RestSharp;
using demo.TestScripts.API;
using RazorEngine.Compilation.ImpromptuInterface;
using System.Collections;
using static demo.Utility.Utils;
using static demo.Data.FilePaths;
using demo.Data;

namespace demo.Config
{

    [SetUpFixture]
    public abstract class TestBase
    {
        public static IWebDriver? _browserDriver;


        [OneTimeSetUp]
        protected void BeforeAllTests()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            string reportFolder = Path.Combine(projectRootPath, @"Report");
            DeleteFiles(reportFolder);

            string reportScreenshots = Path.Combine(projectRootPath, @"Report\Screenshots");
            if (!Directory.Exists(Path.GetDirectoryName(reportScreenshots)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(reportScreenshots));
            }
        }


        [SetUp]
        protected void BeforeTest()
        {
            ReadData read = new ReadData();

            // Start log listener
            Logger.GetExtent();

            // Getting the name of the first category related to the test that is currently executing
            Constants.testCategory = NUnit.Framework.TestContext.CurrentContext.Test.Properties.Get("Category").ToString();
            Console.WriteLine("\nSelected Test Category: " + Constants.testCategory);

            string testSuiteName = "[ " + Constants.environment + " ]  " + Constants.testCategory;

            read.ReadTestData();

            Logger.StartTestReport("");
        }


        [TearDown]
        protected void AfterTest()
        {
            Logger.EndTestReport();

            RestHelper.response = new RestResponse();

            CloseBrowserDriver();
        }


        [OneTimeTearDown]
        protected void AfterAllTests()
        {
            CreateZip(Logger.reportZipName);

            Trace.Flush();

            CloseBrowserDriver();
        }
    }
}
