using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Fuji.BDDTests.Steps
{
    [Binding]
    public sealed class EatingApplesSteps
    {
        private readonly ScenarioContext _ctx;
        private string _hostBaseName = @"https://localhost:44347/";//5001/";
        private readonly IWebDriver _driver;

        public class TestApple
        {
            public string VarietyName { get; set; }
            public string ScientificName { get; set; }
        }

        public EatingApplesSteps(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _ctx = scenarioContext;
            _driver = driver;

            // Don't duplicate if it was already created in another step file
            // Another reason to use a different design
            //if (!_ctx.ContainsKey("WebDriver"))
            //{
            //    FirefoxOptions options = new FirefoxOptions();
            //    options.AcceptInsecureCertificates = true;
            //    _ctx["WebDriver"] = new FirefoxDriver("C:\\Users\\morses", options);

            //    //ChromeOptions options = new ChromeOptions();
            //    //options.AcceptInsecureCertificates = true;
            //    //_ctx["WebDriver"] = new ChromeDriver(options);
            //}
        }

        // -------------------------- GIVEN -------------------------

        [Given(@"the following apples exist")]
        public void GivenTheFollowingApplesExist(Table table)
        {
            IEnumerable<TestApple> apples = table.CreateSet<TestApple>();
            _ctx["Apples"] = apples;
        }

        [Given(@"I am a user")]
        public void GivenIAmAUser()
        {
            // Doesn't matter which user, so pick one
            IEnumerable<TestUser> users = (IEnumerable<TestUser>)_ctx["Users"];
            _ctx["FirstName"] = users.First().FirstName;
        }

        // -------------------------- WHEN -------------------------

        [Given(@"I am on the '(.*)' page")]
        [When(@"I am on the '(.*)' page")]
        public void WhenIAmOnThePage(string pageName)
        {
            //IWebDriver driver = (IWebDriver)_ctx["WebDriver"];
            if (pageName.Equals("Home"))
            {
                _driver.Navigate().GoToUrl(_hostBaseName);
            }
            else
            {
                ScenarioContext.StepIsPending();
            }
        }

        [When(@"I record eating an apple")]
        public void WhenIRecordEatingAnApple()
        {
            // Pick an apple to eat
            int appleToEatIndex = 1;
            // Select the button for this apple
            IWebElement button = _driver.FindElement(By.Id("listOfApples"))
                                         .FindElements(By.TagName("button"))
                                         .ElementAt(appleToEatIndex);
            // store which apple they ate
            _ctx["AppleEatenText"] = button.Text;
            _ctx["TestAppleEaten"] = ((IEnumerable<TestApple>)_ctx["Apples"]).Where(ta => button.Text.Contains(ta.VarietyName, StringComparison.OrdinalIgnoreCase)).First();
            _ctx["AppleEatenCount"] = -65;  // no idea since it literally isn't in the page anywhere!
            // click the button to eat one
            button.Click();
        }

        // -------------------------- THEN -------------------------

        [Then(@"I can see all the apples")]
        public void ThenICanSeeAllTheApples()
        {
            //IWebDriver driver = (IWebDriver)_ctx["WebDriver"];
            // Get the list of apples, they are buttons within a div with id set, select the inner text
            IEnumerable<string> apples = _driver.FindElement(By.Id("listOfApples"))
                                               .FindElements(By.TagName("button"))
                                               .Select(ab => ab.Text);
            IEnumerable<TestApple> testApples = (IEnumerable<TestApple>)_ctx["Apples"];
            foreach (TestApple ta in testApples)
            {
                // check that each test apple is found in exactly one button
                Assert.That(apples.Count(a => a.Contains(ta.VarietyName, StringComparison.OrdinalIgnoreCase)
                                           && a.Contains(ta.ScientificName, StringComparison.OrdinalIgnoreCase)), Is.EqualTo(1));
            }
        }

        [Then(@"I see the count of that apple has increased by (.*)")]
        public void ThenISeeTheCountOfThatAppleHasIncreasedBy(int p0)
        {
            // Which apple was eaten?
            TestApple ta = (TestApple)_ctx["TestAppleEaten"];
            // And how many were eaten before this point?
            int previouslyEatenCount = (int)_ctx["AppleEatenCount"];
            // Get the entry for the apple that was previously eaten
            IWebElement appleTotal = _driver.FindElement(By.Id("appleTotals"))
                                            .FindElements(By.ClassName("list-group-item"))   // very fragile
                                            .Where(li => li.Text.Contains(ta.VarietyName, StringComparison.OrdinalIgnoreCase))
                                            .First();
            int eatenCount = Int32.Parse(appleTotal.FindElement(By.TagName("span")).Text);
            Assert.That(eatenCount, Is.EqualTo(previouslyEatenCount + 1));
        }
    }
}
