using NUnit.Framework;
using static demo.Drivers.BrowserDriverManager;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V123.DOM;
using NLog;
using demo.Data;
using demo.Config;
using demo.TestScripts.API;
using demo.TestScripts.Web.PageObjects;

namespace demo.TestCases
{

    [TestFixture]
    public class Test : TestBase
    {
        IWebDriver _driver = SetUpWebDriver();


        [Order(0)]
        [TestCase(TestName = "[WEB] Subscribe To The Newsletter"), Category("Demo"), Category("WEB")]
        public void SubscribeToTheNewsletter()
        {
            MainPage mainPage = new MainPage(_driver);
            mainPage.OpenHomePage();

            InsightsPage insightsPage = new InsightsPage(_driver);
            insightsPage.OpenInsights();
            insightsPage.GoToBlog();
            insightsPage.OpenArticle();

            ArticlePage articlePage = new ArticlePage(_driver);
            articlePage.EnterSubscriptionEmail(Constants.email);
            articlePage.ClickSubscribeButton();

            _driver.Navigate().Back();
            insightsPage.GetLinks();
        }


        [Order(1)]
        [TestCase(TestName = "[API] Get Users"), Category("Demo"), Category("API")]
        public void GetUserId()
        {
            Users users = new Users();
            Constants.userID = users.GetUsers(200, "$.[0].id");
        }


        [Order(2)]
        [TestCase(TestName = "[API] Get Titles"), Category("Demo"), Category("API")]
        public void GetPosts()
        {
            Posts posts = new Posts();
            var titles = posts.GetPosts(200, "$.[?(@.userId == " + Constants.userID + ")].title");
        }
    }
}
