using Fuji.BDDTests.src;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Fuji.BDDTests.Steps
{
    [Binding]
    public sealed class FirstExampleStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly Calculator _calculator = new Calculator();
        private int _result;

        public FirstExampleStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        // ---------------- Setup operands ----------------
        [Given(@"the first number is (.*)")]
        public void GivenTheFirstNumberIs(int p0)
        {
            _calculator.FirstNumber = p0;
            _scenarioContext["leftOp"] = p0;
        }

        [Given(@"the second number is (.*)")]
        public void GivenTheSecondNumberIs(int p0)
        {
            _calculator.SecondNumber = p0;
            _scenarioContext["rightOp"] = p0;
        }

        // ---------------- Add, subtract and multiply ----------------
        [When(@"the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            _result = _calculator.Add();
            _scenarioContext["result"] = _calculator.Add((int)_scenarioContext["leftOp"], (int)_scenarioContext["rightOp"]);
        }

        [When(@"the two numbers are subtracted")]
        public void WhenTheTwoNumbersAreSubtracted()
        {
            _result = _calculator.Subtract();
            _scenarioContext["result"] = _calculator.Subtract((int)_scenarioContext["leftOp"], (int)_scenarioContext["rightOp"]);
        }

        [When(@"the two numbers are multiplied")]
        public void WhenTheTwoNumbersAreMultiplied()
        {
            _result = _calculator.Multiply();
            _scenarioContext["result"] = _calculator.Multiply((int)_scenarioContext["leftOp"], (int)_scenarioContext["rightOp"]);
        }

        // ---------------- Confirm the result ----------------
        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int p0)
        {
            Assert.That(_result, Is.EqualTo(p0));
            Assert.That(_scenarioContext["result"], Is.EqualTo(p0));
        }

    }
}
