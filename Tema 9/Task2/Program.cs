using System;
using System.IO;

namespace Task;

public class Program
{
    public static void Main()
    {
        string filePath = @"C:\Users\MSI\Documents\Tema 9\Task2\bin\Debug\net8.0";

        Document doc = new Document("Отчет", "Содержимое отчета за март 2025");

        DocumentFileWriter writer = new DocumentFileWriter(filePath);

        Console.WriteLine("Запись документа:");
        writer.WriteAndProtect(doc);

        Console.WriteLine();
        writer.CheckProtection();

        Console.WriteLine("\nПопытка перезаписать защищенный файл:");

        try
        {
            File.WriteAllText(filePath, "Попытка перезаписи");
            Console.WriteLine("Файл был перезаписан");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("\nОчистка...");
        FileInfo info = new FileInfo(filePath);
        info.IsReadOnly = false;
        File.Delete(filePath);
        Console.WriteLine("Файл удален");
    }
}