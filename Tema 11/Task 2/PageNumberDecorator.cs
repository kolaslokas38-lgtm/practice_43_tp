namespace Task;

public class PageNumberDecorator : DocumentDecorator
{
    private int pageNumber;

    public PageNumberDecorator(IDocument document, int pageNumber) : base(document)
    {
        this.pageNumber = pageNumber;
    }

    public override string GetFormattedText()
    {
        return $"{document.GetFormattedText()}\n\nСтраница {pageNumber}";
    }
}