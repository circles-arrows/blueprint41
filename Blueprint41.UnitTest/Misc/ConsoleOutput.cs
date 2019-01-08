using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blueprint41.UnitTest.Misc
{
    public class ConsoleOutput : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOuput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }

    public static class Helper
    {
        public static void AssertThrows<T>(TestDelegate testDelegate, string expectedExceptionMessage = null) where T : Exception
        {
            if (string.IsNullOrEmpty(expectedExceptionMessage))
            {
                T exception = NUnit.Framework.Assert.Throws<T>(testDelegate);
                return;
            }

            Assert.That(testDelegate, Throws.TypeOf<T>()
                .With.Property("Message").EqualTo(expectedExceptionMessage));
        }
    }
}
