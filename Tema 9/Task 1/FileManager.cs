using System;
using System.IO;
using System.Collections.Generic;

namespace Task;

public class FileManager
{
    public void CreateFile(string path, string content)
    {
        File.WriteAllText(path, content);
        Console.WriteLine($"Создан: {path}");
    }

    public void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Console.WriteLine($"Удален: {path}");
        }
        else
        {
            Console.WriteLine($"Файл не найден: {path}");
        }
    }

    public void CopyFile(string source, string dest)
    {
        File.Copy(source, dest, true);
        Console.WriteLine($"Скопирован: {source} -> {dest}");
    }

    public void MoveFile(string source, string dest)
    {
        File.Move(source, dest);
        Console.WriteLine($"Перемещен: {source} -> {dest}");
    }

    public void RenameFile(string path, string newName)
    {
        string directory = Path.GetDirectoryName(path);
        string newPath = Path.Combine(directory, newName);
        File.Move(path, newPath);
        Console.WriteLine($"Переименован: {path} -> {newPath}");
    }

    public void DeleteFilesByPattern(string directory, string pattern)
    {
        string[] files = Directory.GetFiles(directory, pattern);

        foreach (string file in files)
        {
            File.Delete(file);
            Console.WriteLine($"Удален: {file}");
        }
    }

    public List<string> GetFiles(string directory)
    {
        return new List<string>(Directory.GetFiles(directory));
    }
}