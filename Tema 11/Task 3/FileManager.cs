using System;
using System.IO;

namespace Task;

public class FileManager
{
    public void CopyFile(string source, string destination)
    {
        if (!File.Exists(source))
        {
            Console.WriteLine($"Ошибка: файл {source} не существует");
            return;
        }

        File.Copy(source, destination, true);
        Console.WriteLine($"Копирован: {source} -> {destination}");
    }

    public void MoveFile(string source, string destination)
    {
        if (!File.Exists(source))
        {
            Console.WriteLine($"Ошибка: файл {source} не существует");
            return;
        }

        File.Move(source, destination, true);
        Console.WriteLine($"Перемещен: {source} -> {destination}");
    }
}