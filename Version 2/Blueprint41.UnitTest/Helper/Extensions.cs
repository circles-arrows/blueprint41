using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.UnitTest.Tests;
using NUnit.Framework;

using Async = Blueprint41.UnitTest.Tests.Async;
using Blocking = Blueprint41.UnitTest.Tests.Blocking;

namespace Blueprint41.UnitTest
{
    public static class Extensions
    {
        public static void AssertSuccess(this List<Async.TestScenario> scenarios)
        {
            Assert.IsFalse(scenarios.Exists(scenario => scenario.Error));
        }
        public static void AssertSuccess(this List<Blocking.TestScenario> scenarios)
        {
            Assert.IsFalse(scenarios.Exists(scenario => scenario.Error));
        }
    }
}
