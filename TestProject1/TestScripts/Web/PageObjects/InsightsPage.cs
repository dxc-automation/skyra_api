using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using static demo.TestScripts.Web.Helpers.ActionsElements;

namespace demo.TestScripts.Web.PageObjects
{
    public class InsightsPage
    {
        private readonly IWebDriver _driver;
        private static Logger _logger = LogManager.GetCurrentClassLogger();


        public InsightsPage(IWebDriver driver)
        {
            _driver = driver;
        }


        // Elements
        private readonly By MenuInsightsBtn = By.Id("menu-item-4436");
        private readonly By GoToBlogBtn = By.XPath("//a[contains(text(),'Go to blog')]");
        private readonly By ArticleTitle = By.XPath("//a[normalize-space()='Why fintech in Latin America is booming']");


        // Actions
        public void OpenInsights()
        {
            IsElementFound(_driver, MenuInsightsBtn);
            ActionClick(_driver, MenuInsightsBtn);
        }


        public void GoToBlog()
        {
            IsElementFound(_driver, GoToBlogBtn);
            ActionClick(_driver, GoToBlogBtn);
        }


        public void GetLinks()
        {
            List<IWebElement> elements = _driver.FindElements(By.TagName("a")).ToList();

            foreach (var element in elements)
            {
                string title = element.Text;
                string link = element.GetAttribute("href");
                _logger.Info(title + "\n" + link);
            }
        }


        public void OpenArticle()
        {
            while (true)
            {
                ScrollBy(_driver, 500);

                if (FindElement(_driver, ArticleTitle, 5) != null)
                {
                    break;
                }

            }

            ActionClick(_driver, ArticleTitle);
            Thread.Sleep(1000);
            string url = _driver.Url;
            NUnit.Framework.Assert.That(url, Is.EqualTo("https://blankfactor.com/insights/blog/fintech-in-latin-america/"));
        }
    }
}
