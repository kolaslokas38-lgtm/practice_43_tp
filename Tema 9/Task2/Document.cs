namespace Task;

public class Document
{
    public string Title { get; set; }
    public string Content { get; set; }

    public Document(string title, string content)
    {
        Title = title;
        Content = content;
    }

    public override string ToString()
    {
        return $"{Title}\n{Content}";
    }
}