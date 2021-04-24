using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PerformanceTesting
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void RunTests()
        {
            BenchmarkRunner.Run<TestSum>();
            BenchmarkRunner.Run<TestLetterACount>();
        }
    }
}
