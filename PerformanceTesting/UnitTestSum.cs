using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PerformanceTesting.Config;

namespace PerformanceTesting
{
    [Config(typeof(TestConfig))]
    public class TestSum
    {
        [Benchmark(Description = "Summ100")]
        public int Test100()
        {
            return Enumerable.Range(1, 100).Sum();
        }

        [Benchmark(Description = "Summ200")]
        //[Benchmark(Description = "Summ200", Baseline = true)] to use as a base metric = 1 for relative comparison
        public int Test200()
        {
            return Enumerable.Range(1, 200).Sum();
        }
    }

    [TestClass]
    public class UnitTestSum
    {
        [TestMethod]
        public void RunTest()
        {
            BenchmarkRunner.Run<TestSum>();
        }
    }
}
