﻿using BenchmarkDotNet.Attributes;

namespace PerformanceTesting
{
    [Config(typeof(TestConfig))]
    public class TestLetterACount
    {
        [Params("habrahabr", "geektimes", "toster", "megamozg")]
        public string arg;

        [Benchmark(Description = "TestLetterACount")]
        public int CountLetterAIncludings()
        {
            int res = 0;
            for (int i = 0; i < arg.Length; i++)
            {
                if (arg[i] == 'a') { res++; }
            }
            return res;
        }
    }
}
