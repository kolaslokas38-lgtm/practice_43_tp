using System.Text;

namespace Task;

public class TextReport<T> : IReport<T>
{
    public string Generate(T data)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Данные: {data}");
        sb.AppendLine($"Тип: {typeof(T).Name}");

        return sb.ToString();
    }
}