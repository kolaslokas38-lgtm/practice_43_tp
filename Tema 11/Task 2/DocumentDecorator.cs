namespace Task;

public abstract class DocumentDecorator : IDocument
{
    protected IDocument document;

    protected DocumentDecorator(IDocument document)
    {
        this.document = document;
    }

    public abstract string GetFormattedText();
}