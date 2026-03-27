using System;
using System.IO;

namespace Task;

public class DocumentFileWriter
{
    private string filePath;

    public DocumentFileWriter(string filePath)
    {
        this.filePath = filePath;
    }

    public void WriteAndProtect(Document document)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(document.Title);
                writer.WriteLine(document.Content);
            }

            Console.WriteLine($"Документ записан: {filePath}");

            FileInfo fileInfo = new FileInfo(filePath);
            fileInfo.IsReadOnly = true;

            Console.WriteLine($"Файл защищен от перезаписи (ReadOnly)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка записи: {ex.Message}");
        }
    }

    public void CheckProtection()
    {
        if (File.Exists(filePath))
        {
            FileInfo info = new FileInfo(filePath);
            Console.WriteLine($"Файл: {filePath}");
            Console.WriteLine($"  Только чтение: {info.IsReadOnly}");
        }
        else
        {
            Console.WriteLine($"Файл не существует: {filePath}");
        }
    }
}