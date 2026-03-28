namespace Task;

public class PieChartBuilder : IGraphBuilder
{
    private Graph graph;

    public PieChartBuilder()
    {
        graph = new Graph();
        graph.Type = "Круговая диаграмма";
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
        Console.WriteLine("Построение круговой диаграммы");
    }

    public Graph GetGraph()
    {
        return graph;
    }
}