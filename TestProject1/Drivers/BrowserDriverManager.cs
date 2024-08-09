using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using WebDriverManager;
using System;
using OpenQA.Selenium.Remote;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Chrome;
using System.IO;
using demo.Data;

using static demo.Config.TestBase;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace demo.Drivers
{
    public class BrowserDriverManager
    {

        public static IWebDriver SetUpWebDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--incognito");
            options.AddArgument("no-sandbox");
            options.AddArgument("start-maximized");
            options.AddArgument("--disable-search-engine-choice-screen");
            options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
            _browserDriver = new ChromeDriver(options);

            return _browserDriver;
        }


        public static void CloseBrowserDriver()
        {
            try
            {
                _browserDriver.Close();
                _browserDriver.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Browser was already stopped");
            }
        }
    }
}

