namespace WNADotNetCore.ChartWebApp.Models
{
    public class ApexAreaChartModel
    {
        public required List<ChartSeries> Series { get; set; }
        public required List<string> Categories { get; set; }
        public required string Title { get; set; }
        public required string XAxisType { get; set; }
    }

    public class ChartSeries
    {
        public required string Name { get; set; }
        public required List<int> Data { get; set; }
    }

    public class ApexBarChartModel
    {
        public required List<int> Data { get; set; }
        public required List<string> Categories { get; set; }
        public required string Title { get; set; }
    }

    public class ApexDistributedChartModel
    {
        public required List<int> Data { get; set; }
        public required List<string> Categories { get; set; }
        public required List<string> Colors { get; set; }
    }

    public class ApexPieChartModel
    {
        public required List<int> Series { get; set; }
        public required List<string> Labels { get; set; }
        public required string Title { get; set; }
    }
}
