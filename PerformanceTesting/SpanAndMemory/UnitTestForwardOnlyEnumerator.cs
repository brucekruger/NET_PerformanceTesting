using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PerformanceTesting.SpanAndMemory
{
    [Config(typeof(TestConfig))]
    public class TestForwardOnlyEnumerator
    {
        private const string TestString = "The quick brown fox jumps over the lazy dog";

        [Benchmark(Description = nameof(TestStandardEnumerator))]
        public void TestStandardEnumerator()
        {
            foreach (var word in TestString.Split())
            {
                string phrasePart = word;
            }
        }

        [Benchmark(Description = nameof(TestForward_OnlyEnumerator))]
        public void TestForward_OnlyEnumerator()
        {
            foreach (var word in TestString.AsSpan().Split())
            {
                ReadOnlySpan<char> phrasePart = word;
            }
        }
    }

    public readonly ref struct CharSpanSplitter
    {
        readonly ReadOnlySpan<char> _input;
        public CharSpanSplitter(ReadOnlySpan<char> input) => _input = input;
        public Enumerator GetEnumerator() => new Enumerator(_input);
        public ref struct Enumerator // Forward-only enumerator
        {
            readonly ReadOnlySpan<char> _input;
            int _wordPos;
            public ReadOnlySpan<char> Current { get; private set; }
            public Enumerator(ReadOnlySpan<char> input)
            {
                _input = input;
                _wordPos = 0;
                Current = default;
            }
            public bool MoveNext()
            {
                for (int i = _wordPos; i <= _input.Length; i++)
                    if (i == _input.Length || char.IsWhiteSpace(_input[i]))
                    {
                        Current = _input[_wordPos..i];
                        _wordPos = i + 1;
                        return true;
                    }
                return false;
            }
        }
    }

    public static class CharSpanExtensions
    {
        public static CharSpanSplitter Split(this ReadOnlySpan<char> input)
            => new CharSpanSplitter(input);
        public static CharSpanSplitter Split(this Span<char> input)
            => new CharSpanSplitter(input);
    }

    [TestClass]
    public class UnitTestForwardOnlyEnumerator
    {
        [TestMethod]
        public void RunTest()
        {
            BenchmarkRunner.Run<TestForwardOnlyEnumerator>();
        }
    }
}
