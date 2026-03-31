namespace Task;

public class PlainDocument : IDocument
{
    private string content;
    
    public PlainDocument(string content)
    {
        this.content = content;
    }

    public string GetFormattedText()
    {
        return content;
    }
}