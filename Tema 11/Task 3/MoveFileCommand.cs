
namespace Task;

public class MoveFileCommand : ICommand
{
    private FileManager receiver;
    private string source;
    private string destination;

    public MoveFileCommand(FileManager receiver, string source, string destination)
    {
        this.receiver = receiver;
        this.source = source;
        this.destination = destination;
    }

    public void Execute()
    {
        receiver.MoveFile(source, destination);
    }
}