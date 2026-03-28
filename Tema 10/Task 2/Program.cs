using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        IGraphBuilder[] builders =
        [
            new LineGraphBuilder(),
            new BarGraphBuilder(),
            new PieChartBuilder()
        ];

        foreach (var builder in builders)
        {
            builder.SetTitle("Продажи 2025");
            builder.AddDataPoint(100, "Янв");
            builder.AddDataPoint(150, "Фев");
            builder.AddDataPoint(200, "Мар");
            builder.Build();

            Graph graph = builder.GetGraph();
            graph.Display();
        }
    }
}