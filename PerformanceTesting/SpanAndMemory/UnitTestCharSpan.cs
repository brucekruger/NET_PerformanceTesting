using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PerformanceTesting.SpanAndMemory
{
    [Config(typeof(TestConfig))]
    public class TestSpanChar
    {
        private const string TestString = "Word1 Word2 Word3 Word4 Word5 Word6 Word7 Word8 Word9 Word10";

        [Benchmark(Description = nameof(TestCountCharWhitespaces))]
        public void TestCountCharWhitespaces()
        {
            int result = CountWhitespace(TestString.ToCharArray());
        }

        [Benchmark(Description = nameof(TestCountSpanWhitespaces))]
        public void TestCountSpanWhitespaces()
        {
            int result = CountWhitespaceSpan(TestString.ToCharArray());
        }

        private int CountWhitespace(char[] s)
        {
            int count = 0;
            foreach (char c in s)
                if (char.IsWhiteSpace(c))
                    count++;
            return count;
        }

        private int CountWhitespaceSpan(ReadOnlySpan<char> s)
        {
            int count = 0;
            foreach (char c in s)
                if (char.IsWhiteSpace(c))
                    count++;
            return count;
        }
    }


    [TestClass]
    public class UnitTestSpanChar
    {
        [TestMethod]
        public void RunTest()
        {
            BenchmarkRunner.Run<TestSpanChar>();
        }
    }
}
