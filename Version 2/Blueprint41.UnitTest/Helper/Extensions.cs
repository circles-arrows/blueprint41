﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.UnitTest.Tests;
using NUnit.Framework;

namespace Blueprint41.UnitTest
{
    public static class Extensions
    {
        public static void AssertSuccess(this List<TestScenario> scenarios)
        {
            Assert.IsFalse(scenarios.Exists(scenario => scenario.Error));
        }
    }
}
