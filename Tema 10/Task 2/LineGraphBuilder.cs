namespace Task;

public class LineGraphBuilder : IGraphBuilder
{
    private Graph graph;

    public LineGraphBuilder()
    {
        graph = new Graph();
        graph.Type = "Линейный график";
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
        Console.WriteLine("Построение линейного графика...");
    }

    public Graph GetGraph()
    {
        return graph;
    }
}