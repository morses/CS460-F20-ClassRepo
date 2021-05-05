using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Fuji.BDDTests.Steps
{
    [Binding]
    public sealed class EatingApplesSteps
    {

        private readonly ScenarioContext _ctx;

        public EatingApplesSteps(ScenarioContext scenarioContext)
        {
            _ctx = scenarioContext;
        }
    }
}
