using System.Collections.Generic;

namespace Task;

public class Graph
{
    public string Title { get; set; }
    public string Type { get; set; }
    public List<int> Data { get; set; }
    public List<string> Labels { get; set; }

    public Graph()
    {
        Data = new List<int>();
        Labels = new List<string>();
    }

    public void Display()
    {
        Console.WriteLine($"\n=== {Title} ===");
        Console.WriteLine($"Тип: {Type}");

        for (int i = 0; i < Data.Count; i++)
        {
            string label = i < Labels.Count ? Labels[i] : $"Point {i + 1}";
            Console.WriteLine($"  {label}: {Data[i]}");
        }
    }
}