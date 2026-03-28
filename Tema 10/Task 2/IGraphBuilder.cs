namespace Task;

public interface IGraphBuilder
{
    void SetTitle(string title);
    void AddDataPoint(int value, string label);
    void Build();
    Graph GetGraph();
}