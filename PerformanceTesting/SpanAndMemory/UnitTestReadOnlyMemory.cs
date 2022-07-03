using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PerformanceTesting.SpanAndMemory
{
    [Config(typeof(TestConfig))]
    public class TestReadOnlyMemory
    {
        private const string TestString = "The quick brown fox jumps over the lazy dog";

        [Benchmark(Description = nameof(TestSplit))]
        public void TestSplit()
        {
            string[] result = TestString.Split();
        }

        [Benchmark(Description = nameof(TestReadOnlyMemorySplit))]
        public void TestReadOnlyMemorySplit()
        {
            IEnumerable<ReadOnlyMemory<char>> result = Split(TestString.ToCharArray());
        }

        // Split a string into words:
        private IEnumerable<ReadOnlyMemory<char>> Split(ReadOnlyMemory<char> input)
        {
            int wordStart = 0;
            for (int i = 0; i <= input.Length; i++)
                if (i == input.Length || char.IsWhiteSpace(input.Span[i]))
                {
                    yield return input[wordStart..i]; // Slice with C# range operator
                    wordStart = i + 1;
                }
        }
    }


    [TestClass]
    public class UnitTestReadOnlyMemory
    {
        [TestMethod]
        public void RunTest()
        {
            BenchmarkRunner.Run<UnitTestReadOnlyMemory>();
        }
    }
}
