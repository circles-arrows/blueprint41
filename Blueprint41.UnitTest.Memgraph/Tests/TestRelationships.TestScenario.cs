using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Blueprint41.UnitTest.Memgraph.Tests
{
    [DebuggerDisplay("{ToString()}")]
    public class TestScenario
    {
        private TestScenario(int mask, DateTime moment, TestAction action)
        {
            Mask = mask;
            Initial = RelationsFromMask(mask);
            InitialAsciiArt = DrawAsciiArtState(Initial);
            Moment = moment;
            MomentAsciiArt = DrawAsciiArtMoment(moment);
            switch (action)
            {
                case TestAction.AddSame:
                    Expected = RelationsFromMask(AdjustMaskForAdd(mask, moment));
                    ExpectedEx = Expected.Select(x => (x.from, x.till, PropertySet.Same)).ToList();
                    break;
                case TestAction.AddDiff:
                    var set1 = RelationsFromMask(AdjustMaskForRemove(mask, moment));
                    var set2 = RelationsFromMask(AdjustMaskForAdd(0, moment));
                    Expected = set1.Union(set2).ToList();
                    ExpectedEx = set1.Select(x => (x.from, x.till, PropertySet.Before)).Union(set2.Select(x => (x.from, x.till, PropertySet.After))).ToList();
                    break;
                case TestAction.Remove:
                    Expected = RelationsFromMask(AdjustMaskForRemove(mask, moment));
                    ExpectedEx = Expected.Select(x => (x.from, x.till, PropertySet.Same)).ToList();
                    break;
                default:
                    throw new NotImplementedException();
            }
            ExpectedAsciiArt = DrawAsciiArtState(Expected);
            Actual = null;
            ActualAsciiArt = null;
            Action = action;
            Error = false;
        }
        private TestScenario(TestScenario original)
        {
            Mask = original.Mask;
            Initial = original.Initial;
            InitialAsciiArt = original.InitialAsciiArt;
            Moment = original.Moment;
            MomentAsciiArt = original.MomentAsciiArt;
            Expected = original.Expected;
            ExpectedEx = original.ExpectedEx;
            ExpectedAsciiArt = original.ExpectedAsciiArt;
            Actual = null;
            ActualAsciiArt = null;
            Action = original.Action;
            Error = false;
        }

        public int Mask { get; private set; }
        public List<(DateTime from, DateTime till)> Initial { get; private set; }
        public string InitialAsciiArt { get; private set; }
        public DateTime Moment { get; private set; }
        public string MomentAsciiArt { get; private set; }
        public TestAction Action { get; private set; }
        public List<(DateTime from, DateTime till)> Expected { get; private set; }
        private List<(DateTime from, DateTime till, PropertySet set)> ExpectedEx { get; set; }
        public string ExpectedAsciiArt { get; private set; }
        public List<(DateTime from, DateTime till)> Actual { get; private set; }
        public string ActualAsciiArt { get; private set; }
        public bool Error { get; private set; }

        public static List<(DateTime from, DateTime till)> RelationsFromMask(int mask)
        {
            DateTime? from = null;
            var relations = new List<(DateTime from, DateTime till)>();

            for (int pos = 0; pos <= bits; pos++)
            {
                bool bit = ((1 << pos) & mask) != 0;
                if (bit && from is null)
                {
                    from = dates[pos];
                }
                else if (!bit && from is not null)
                {
                    relations.Add((from.Value, dates[pos]));
                    from = null;
                }
            }

            return relations;
        }
        private static int AdjustMaskForAdd(int mask, DateTime moment)
        {
            int retval = mask;

            int idx = Array.IndexOf(dates, moment);
            while (idx < bits)
            {
                retval |= (1 << idx);
                idx++;
            }

            return retval;
        }
        private static int AdjustMaskForRemove(int mask, DateTime moment)
        {
            int retval = mask;

            int idx = Array.IndexOf(dates, moment);
            while (idx < bits)
            {
                retval &= ~(1 << idx);
                idx++;
            }

            return retval;
        }

        public static string DrawAsciiArtState(List<(DateTime from, DateTime till)> relations)
        {
            char[] asciiArt = new char[(bits << 2) + 1];
            for (int index = 0; index < asciiArt.Length; index++)
                asciiArt[index] = ' ';

            foreach ((DateTime from, DateTime till) in relations)
            {
                int fromIdx = Array.IndexOf(dates, from);
                int tillIdx = Array.IndexOf(dates, till);

                int fromPos = fromIdx << 2;
                int tillPos = tillIdx << 2;

                char fromChr = (fromIdx == 0) ? '<' : '|';
                char tillChr = (tillIdx == bits) ? '>' : '|';

                for (int pos = fromPos; pos <= tillPos; pos++)
                {
                    if (pos == fromPos)
                        asciiArt[pos] = fromChr;
                    else if (pos == tillPos)
                        asciiArt[pos] = tillChr;
                    else
                        asciiArt[pos] = '-';
                }
            }

            return new string(asciiArt);
        }
        private static string DrawAsciiArtMoment(DateTime moment)
        {
            char[] asciiArt = new char[(bits << 2) + 1];
            for (int index = 0; index < asciiArt.Length; index++)
                asciiArt[index] = ' ';

            int idx = Array.IndexOf(dates, moment);
            int pos = idx << 2;

            char chr = '|';
            if (idx == 0) chr = '<';
            if (idx == bits) chr = '>';

            asciiArt[pos] = chr;

            return new string(asciiArt);
        }

        public static readonly DateTime[] dates = new[]
        {
                Conversion.MinDateTime,
                new DateTime(1981, 4, 12, 12, 0, 4, DateTimeKind.Utc), // The first orbital flight of the space shuttle, NASA's reusable space vehicle.
                new DateTime(1990, 4, 24, 12, 33, 51, DateTimeKind.Utc), // Apr 25, 1990 - Hubble Space Telescope launched into space.
                new DateTime(2012, 8, 25, 0, 0, 0, DateTimeKind.Utc), // Aug 25, 2012 - Voyager 1 becomes the first spacecraft to reach interstellar space.
                Conversion.MaxDateTime,
            };
        public static readonly int bits = dates.Length - 1;
        public static readonly int count = (1 << bits);

        public void SetActual(List<(DateTime from, DateTime till)> actual)
        {
            Actual = actual;
            ActualAsciiArt = DrawAsciiArtState(actual);
            Error = (ActualAsciiArt != ExpectedAsciiArt);
        }
        public PropertySet TestSet(DateTime from, DateTime till)
        {
            PropertySet? set = ExpectedEx.Where(x => x.from == from && x.till == till).Select(x => (PropertySet?)x.set).FirstOrDefault();
            return set ?? PropertySet.Same;
        }

        public static List<TestScenario> Get(params TestAction[] actions)
        {
            List<TestScenario> result = new List<TestScenario>();

            for (int mask = 0; mask < count; mask++)
            {
                foreach (TestAction action in actions)
                {
                    foreach (DateTime moment in dates)
                    {
                        result.Add(new TestScenario(mask, moment, action));
                    }
                }
            }

            return result;
        }


        public override string ToString()
        {
            if (Error)
                return $"Initial: '{InitialAsciiArt}', Moment: '{MomentAsciiArt}', Action: {Action}, Expected: '{ExpectedAsciiArt}', Actual: '{ActualAsciiArt}', UNEXPECTED STATE";

            return $"Initial: '{InitialAsciiArt}', Moment: '{MomentAsciiArt}', Action: {Action}, Expected: '{ExpectedAsciiArt}', Actual: '{ActualAsciiArt}'";
        }
        public TestScenario Clone()
        {
            return new TestScenario(this);
        }
    }
    public enum TestAction
    {
        AddSame,
        AddDiff,
        Remove,
    }
    public enum PropertySet
    {
        Before,
        After,
        Same,
    }
}
