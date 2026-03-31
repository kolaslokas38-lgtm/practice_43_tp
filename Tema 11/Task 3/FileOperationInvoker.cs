using System.Collections.Generic;

namespace Task;

public class FileOperationInvoker
{
    private List<ICommand> commands = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        commands.Add(command);
        System.Console.WriteLine($"Команда добавлена в очередь");
    }

    public void ExecuteAll()
    {
        System.Console.WriteLine("\nВыполнение всех команд:");

        foreach (var command in commands)
        {
            command.Execute();
        }

        commands.Clear();
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
    }
}