using AngleSharp.Dom;
using Microsoft.VisualBasic;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Net;

namespace demo.TestScripts.Web.Helpers
{
    public class ActionsElements
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public static WebDriverWait wait;

        public static IList<IWebElement>? LinkElements { get; set; }


        /// <summary>
        /// Check is element displayed and returns boolean
        /// </summary>
        public static bool IsElementFound(IWebDriver driver, By locator)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                _logger.Info(locator + " has been successfully localized");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return false;
            }
        }


        public static void LoopLinks(IWebDriver driver)
        {
            List<IWebElement> links = new List<IWebElement>();
            links = driver.FindElements(By.TagName("a")).ToList();

            foreach (IWebElement link in links)
            {
                Console.WriteLine(link.Text);
            }
        }


        public static void WaitForPageLoad(IWebDriver driver, int timeout)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeout));
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }


        public static IWebElement WaitForElementToDisplay(IWebDriver driver, By locator, int timeout)
        {
            DefaultWait<IWebDriver> fluentwait = new DefaultWait<IWebDriver>(driver);
            fluentwait.Timeout = TimeSpan.FromSeconds(timeout);
            fluentwait.PollingInterval = TimeSpan.FromSeconds(3);
            fluentwait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            fluentwait.Message = "Element not found";

            IWebElement element = driver.FindElement(locator);
            return element;
        }


        public static IWebElement FindElement(IWebDriver driver, By locator, int timeout)
        {
            IWebElement element = null;
            try
            {
                element = WaitForElementToDisplay(driver, locator, timeout);

                Console.WriteLine("Stalement Element expection occured, re-trying to find element");
                WaitForPageLoad(driver, timeout);
                element = WaitForElementToDisplay(driver, locator, timeout);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return element;
        }


        public static void ScrollToView(IWebDriver driver, IWebElement element)
        {
            /*
             * If this method is not working for you, use following code
             * ((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", element);
             */
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);

        }


        public static void ScrollBy(IWebDriver driver, int pixels)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, " + pixels + ")");
        }


        public static void ScrollToBottom(IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,document.body.scrollHeight)");
        }


        public static void ActionClick(IWebDriver driver, By locator)
        {
            Actions actions = new Actions(driver);
            IWebElement element = driver.FindElement(locator);
            actions.MoveToElement(element).Click().Build().Perform();
        }


        public static void Click(IWebDriver driver, By locator, int timeout)
        {
            try
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                wait.Until(ExpectedConditions.ElementToBeClickable(locator));

                IWebElement element = WaitForElementToDisplay(driver, locator, timeout);
                if (element != null)
                {
                    ScrollToView(driver, element);
                    element.Click();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
