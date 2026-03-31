namespace Task;

public class FooterDecorator : DocumentDecorator
{
    private string footer;

    public FooterDecorator(IDocument document, string footer) : base(document)
    {
        this.footer = footer;
    }

    public override string GetFormattedText()
    {
        return $"{document.GetFormattedText()}\n{new string('-', footer.Length)}\n{footer}";
    }
}