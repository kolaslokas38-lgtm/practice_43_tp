namespace Task;

public class HeaderDecorator : DocumentDecorator
{
    private string header;

    public HeaderDecorator(IDocument document, string header) : base(document)
    {
        this.header = header;
    }

    public override string GetFormattedText()
    {
        return $"{header}\n{new string('-', header.Length)}\n{document.GetFormattedText()}";
    }
}