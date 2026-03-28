namespace Task;

public class BarGraphBuilder : IGraphBuilder
{
    private Graph graph;

    public BarGraphBuilder()
    {
        graph = new Graph();
        graph.Type = "Столбчатая диаграмма";
    }

    public void SetTitle(string title)
    {
        graph.Title = title;
    }

    public void AddDataPoint(int value, string label)
    {
        graph.Data.Add(value);
        graph.Labels.Add(label);
    }

    public void Build()
    {
        Console.WriteLine("Построение столбчатой диаграммы");
    }

    public Graph GetGraph()
    {
        return graph;
    }
}