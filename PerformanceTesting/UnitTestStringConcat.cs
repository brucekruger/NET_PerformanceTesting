using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using PerformanceTesting.Config;

namespace PerformanceTesting
{
    [Config(typeof(TestConfig))]
    public class TestStringConcat
    {
        const string SomeStringData = "QWERTYUIOPASDFGHJKLZXCVBNM";
        const int NumberOfOperations = 10000;

        [Benchmark(Description = "TestRegularString")]
        public int TestRegularString()
        {
            return UseRegularString(NumberOfOperations);
        }

        [Benchmark(Description = "TestStringBuilder")]
        public int TestStringBuilder()
        {
            return UseStringBuilder(NumberOfOperations);
        }

        public int UseRegularString(int numberOfOperations)
        {
            string myStringData = null;
            for (int cnt = 0; cnt < numberOfOperations; cnt++)
            {
                myStringData += SomeStringData;
            }
            return myStringData.Length;
        }

        public int UseStringBuilder(int numberOfOperations)
        {
            var myStringData = new StringBuilder();
            for (int cnt = 0; cnt < numberOfOperations; cnt++)
            {
                myStringData.Append(SomeStringData);
            }
            return myStringData.Length;
        }
    }

    [TestClass]
    public class UnitTestStringConcat
    {
        [TestMethod]
        public void RunTest()
        {
            BenchmarkRunner.Run<TestStringConcat>();
        }
    }
}
