using BenchmarkDotNet.Attributes;
using System.Linq;

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
}
