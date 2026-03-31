

namespace Task;

public class CopyFileCommand : ICommand
{
    private FileManager receiver;
    private string source;
    private string destination;

    public CopyFileCommand(FileManager receiver, string source, string destination)
    {
        this.receiver = receiver;
        this.source = source;
        this.destination = destination;
    }

    public void Execute()
    {
        receiver.CopyFile(source, destination);
    }
}