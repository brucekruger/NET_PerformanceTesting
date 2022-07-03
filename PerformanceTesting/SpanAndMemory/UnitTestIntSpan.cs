using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PerformanceTesting.Config;

namespace PerformanceTesting.SpanAndMemory
{
    [Config(typeof(TestConfig))]
    public class TestSpanInt
    {
        [Benchmark(Description = nameof(TestIntArraySum))]
        public void TestIntArraySum()
        {
            var numbers = new int[10000];
            for (int i = 0; i < numbers.Length; i++) numbers[i] = i;
            int total = Sum(numbers);
        }

        [Benchmark(Description = nameof(TestSpanIntSum))]
        public void TestSpanIntSum()
        {
            var numbers = new int[10000];
            for (int i = 0; i < numbers.Length; i++) numbers[i] = i;
            int total = SumSpan(numbers.AsSpan());
        }

        [Benchmark(Description = nameof(TestStackAllocSpanIntSum))]
        public void TestStackAllocSpanIntSum()
        {
            Span<int> numbers = stackalloc int[10000];
            for (int i = 0; i < numbers.Length; i++) numbers[i] = i;
            int total = SumSpan(numbers);
        }

        private int Sum(int[] numbers)
        {
            int total = 0;
            int len = numbers.Length;
            for (int i = 0; i < len; i++) total += numbers[i];
            return total;
        }

        private int SumSpan(ReadOnlySpan<int> numbers)
        {
            int total = 0;
            int len = numbers.Length;
            for (int i = 0; i < len; i++) total += numbers[i];
            return total;
        }
    }


    [TestClass]
    public class UnitTestIntSpan
    {
        [TestMethod]
        public void RunTest()
        {
            BenchmarkRunner.Run<TestSpanInt>();
        }
    }
}
