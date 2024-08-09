using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using demo.Data;
using static demo.TestScripts.Web.Helpers.ActionsElements;

namespace demo.TestScripts.Web.PageObjects
{
    public class ArticlePage
    {
        private readonly IWebDriver _driver;

        public ArticlePage(IWebDriver driver)
        {
            _driver = driver;
        }


        // Elements
        private readonly By SubscribeBtn = By.Id("form-newsletter-blog-submit-btn");
        private readonly By SubscribeEmailInput = By.XPath("//input[@placeholder='Your email address']");
        private readonly By SubscriptionResponse = By.XPath("//div[@class='mc4wp-response']");


        // Actions
        public void EnterSubscriptionEmail(string email)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(SubscribeEmailInput)).Click();
            _driver.FindElement(SubscribeEmailInput).SendKeys(email);
        }

        public void ClickSubscribeButton()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(SubscribeBtn)).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(SubscriptionResponse));
        }
    }
}
