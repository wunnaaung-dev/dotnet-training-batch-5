using Microsoft.AspNetCore.Mvc;
using WNADotNetCore.ChartWebApp.Models;

namespace WNADotNetCore.ChartWebApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult AreaChart()
        {
            var model = new ApexAreaChartModel()
            {
                Series = new List<ChartSeries>()
                {
                    new ChartSeries
                    {
                        Name = "Laptop Sales",
                        Data = new List<int> { 31, 40, 28, 51, 42, 109, 100 }
                    },
                    new ChartSeries
                    {
                        Name = "Mobile Phone Sales",
                        Data = new List<int> { 50, 70, 60, 80, 90, 120, 110 }
                    }
                },
                Categories = new List<string>
                {
                    "2023-01-01", "2023-02-01", "2023-03-01",
                    "2023-04-01", "2023-05-01", "2023-06-01", "2023-07-01"
                },
                Title = "Monthly Sales Performance",
                XAxisType = "datetime"
            };
            return View(model);
        }

        public IActionResult BarChart()
        {
            var model = new ApexBarChartModel
            {
                Data = new List<int> { 400, 430, 448, 470, 540, 580, 690, 1100, 1200, 1380 },
                Categories = new List<string>
                {
                    "South Korea", "Canada", "United Kingdom", "Netherlands", "Italy",
                    "France", "Japan", "United States", "China", "Germany"
                },
                Title = "Country GDP Growth"
            };

            return View(model);
        }

        public IActionResult ColumnChart()
        {
            var model = new ApexDistributedChartModel()
            {
                Data = new List<int> { 21, 22, 10, 28, 16},
                Categories = new List<string>
                        {
                            "Sales", "Marketing", "HR", "Finance", "IT"
                        },
                Colors = new List<string> { "#00E396", "#775DD0", "#FF4560", "#FEB019", "#546E7A" }
            };
            return View(model);
        }

        public IActionResult PieChart()
        {
            var model = new ApexPieChartModel()
            {
                Series = new List<int> { 120, 150, 180, 100, 80 },
                Labels = new List<string> { "North America", "Europe", "Asia", "Australia", "Africa" },
                Title = "Regional Sales Breakdown"
            };
            return View(model);
        }
    }
}
