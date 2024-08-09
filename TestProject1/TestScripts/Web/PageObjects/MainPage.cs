using demo.Data;
using NLog;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using static demo.TestScripts.Web.Helpers.ActionsElements;

namespace demo.TestScripts.Web.PageObjects
{
    public class MainPage
    {
        private readonly IWebDriver _driver;

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
        }


        // Elements
        private readonly By PolicyAcceptBtn = By.Id("hs-eu-confirmation-button");


        // Actions
        public void OpenHomePage()
        {
            _driver.Navigate().GoToUrl(Constants.baseUrl);
            IsElementFound(_driver, PolicyAcceptBtn);
            Click(_driver, PolicyAcceptBtn, 15);
        }
    }
}
