using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;

namespace PerformanceTesting
{
    public class TestConfig : ManualConfig
    {
        public TestConfig()
        {
            AddColumn(StatisticColumn.Max); // Adding necessary column
            AddExporter(RPlotExporter.Default, CsvExporter.Default);
        }
    }
}
