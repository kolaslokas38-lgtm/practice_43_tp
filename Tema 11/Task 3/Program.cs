using System;
using System.IO;

namespace Task;

public class Program
{
    public static void Main()
    {
        string testDir = @"C:\Users\MSI\Documents\Tema 11\Task 3\bin\Debug\net8.0";
        Directory.CreateDirectory(testDir);

        string sourceFile = Path.Combine(testDir, "test.txt");
        string copyFile = Path.Combine(testDir, "copy.txt");
        string moveFile = Path.Combine(testDir, "moved.txt");

        File.WriteAllText(sourceFile, "Тестовое содержимое");
        Console.WriteLine($"Создан: {sourceFile}\n");

        FileManager manager = new FileManager();
        FileOperationInvoker invoker = new FileOperationInvoker();

        ICommand copyCommand = new CopyFileCommand(manager, sourceFile, copyFile);
        ICommand moveCommand = new MoveFileCommand(manager, copyFile, moveFile);

        invoker.AddCommand(copyCommand);
        invoker.AddCommand(moveCommand);

        invoker.ExecuteAll();

        Console.WriteLine("Проверка:");
        Console.WriteLine($"Исходный файл существует: {File.Exists(sourceFile)}");
        Console.WriteLine($"Скопированный файл существует: {File.Exists(copyFile)}");
        Console.WriteLine($"Перемещенный файл существует: {File.Exists(moveFile)}");

        Console.WriteLine("\nОтдельное выполнение:");

        ICommand directCopy = new CopyFileCommand(manager, moveFile, Path.Combine(testDir, "direct.txt"));
        invoker.ExecuteCommand(directCopy);

        File.Delete(sourceFile);
        File.Delete(moveFile);
        File.Delete(Path.Combine(testDir, "direct.txt"));
    }
}