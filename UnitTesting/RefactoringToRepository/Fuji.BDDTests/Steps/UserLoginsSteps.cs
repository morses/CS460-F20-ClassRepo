using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Fuji.BDDTests.Steps
{
    public class TestUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }

    [Binding]
    public sealed class UserLoginsSteps
    {

        private readonly ScenarioContext _ctx;
        //private Table _userTable;
        private string _hostBaseName = @"https://localhost:44347/";//5001/";
        private readonly IWebDriver _driver;

        public UserLoginsSteps(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _driver = driver;
            _ctx = scenarioContext;
            //FirefoxOptions options = new FirefoxOptions();
            //options.AcceptInsecureCertificates = true;
            //_ctx["WebDriver"] = new FirefoxDriver(options);

            //    //ChromeOptions options = new ChromeOptions();
            //    //options.AcceptInsecureCertificates = true;
            //    //_ctx["WebDriver"] = new ChromeDriver(options);
        }

        [Given(@"the following users exist")]
        public void GivenTheFollowingUsersExist(Table table)
        {
            // Better to put it in the context than to create a private variable to store shared data
            //_userTable = table;

            // Nothing to actually do for this step, but we do want to save the 
            // user data for steps to follow
            IEnumerable<TestUser> users = table.CreateSet<TestUser>();
            _ctx["Users"] = users;
        }

        [Given(@"the following users do not exist")]
        public void GivenTheFollowingUsersDoNotExist(Table table)
        {
            // Same with this one, just setting up the background data
            IEnumerable<TestUser> nonUsers = table.CreateSet<TestUser>();
            _ctx["NonUsers"] = nonUsers;
        }

        [Given(@"I am a user with first name '(.*)'")]
        public void GivenIAmAUserWithFirstName(string firstName)
        {
            _ctx["FirstName"] = firstName;
        }

        [When(@"I login")]
        [Given(@"I login")]
        public void WhenILogin()
        {
            // Fetch the webdriver (this is tedious, we can make this better)
            //IWebDriver driver = (IWebDriver)_ctx["WebDriver"];
            // Go to the login page (waits until document.readyState is "complete" (but depending on your case it may not yet be "ready")
            _driver.Navigate().GoToUrl(_hostBaseName + @"Identity/Account/Login");
            // Get username and password for the user we're currently testing, look them up in the user table
            // we got in the background
            string firstName = (string)_ctx["FirstName"];
            IEnumerable<TestUser> users = (IEnumerable<TestUser>)_ctx["Users"];
            TestUser u = users.Where(u => u.FirstName == firstName).FirstOrDefault();
            if (u == null)
            {
                // must have been selecting from non-users
                users = (IEnumerable<TestUser>)_ctx["NonUsers"];
                u = users.Where(u => u.FirstName == firstName).FirstOrDefault();
            }
            // Enter email and password into the input fields
            _driver.FindElement(By.Id("Input_Email")).SendKeys(u.Email);
            _driver.FindElement(By.Id("Input_Password")).SendKeys(u.Password);  // could submit form by "hitting enter" with (u.Password + Keys.Enter)
            // can "submit" the form by calling submit on any element in the form or actually click the submit button
            _driver.FindElement(By.Id("account")).FindElement(By.CssSelector("button[type=submit]")).Click();
        }


        // -------------------------- THEN -------------------------

        [Then(@"I am redirected to the '(.*)' page")]
        public void ThenIAmRedirectedToThePage(string pageName)
        {
            //IWebDriver driver = (IWebDriver)_ctx["WebDriver"];
            if (pageName.Equals("Home"))
            {
                Assert.That(_driver.Url, Is.EqualTo(_hostBaseName).IgnoreCase);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Then(@"I can see a personalized message in the navbar that includes my email")]
        public void ThenICanSeeAPersonalizedMessageInTheNavbarThatIncludesMyEmail()
        {
            //IWebDriver driver = (IWebDriver)_ctx["WebDriver"];
            // Look for "Hello <email>!" in the navbar
            string firstName = (string)_ctx["FirstName"];
            IEnumerable<TestUser> users = (IEnumerable<TestUser>)_ctx["Users"];
            TestUser u = users.Where(u => u.FirstName == firstName).First();
            // Enter email and password into the input fields
            IWebElement welcome = _driver.FindElement(By.CssSelector("a[href=\"/Identity/Account/Manage\"]"));
            Assert.That(welcome.Text, Does.Contain(u.Email).IgnoreCase);
        }

        [Then(@"I can see a login error message")]
        public void ThenICanSeeALoginErrorMessage()
        {
            Assert.Fail();
        }

        [Then(@"an unsuccessful login attempt by '(.*)' is logged")]
        public void ThenAnUnsuccessfulLoginAttemptByIsLogged(string p0)
        {
            Assert.Fail();
        }

    }
}
