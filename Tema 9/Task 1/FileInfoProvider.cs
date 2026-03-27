using System;
using System.IO;
using System.Security.AccessControl;

namespace Task;

public class FileInfoProvider
{
    public void GetInfo(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine($"Файл не найден: {path}");
            return;
        }

        FileInfo info = new FileInfo(path);
        Console.WriteLine($"Файл: {path}");
        Console.WriteLine($"  Размер: {info.Length} байт");
        Console.WriteLine($"  Создан: {info.CreationTime}");
        Console.WriteLine($"  Изменен: {info.LastWriteTime}");
    }

    public bool CompareSize(string file1, string file2)
    {
        FileInfo f1 = new FileInfo(file1);
        FileInfo f2 = new FileInfo(file2);
        bool equal = f1.Length == f2.Length;
        Console.WriteLine($"Размеры {file1} и {file2} {(equal ? "равны" : "не равны")}");
        return equal;
    }

    public void CheckPermissions(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine($"Файл не найден: {path}");
            return;
        }

        FileInfo info = new FileInfo(path);
        Console.WriteLine($"Права доступа к {path}:");
        Console.WriteLine($"  Чтение: {(info.IsReadOnly ? "Только чтение" : "Чтение/запись")}");

        try
        {
            FileSecurity security = info.GetAccessControl();
            Console.WriteLine($"  Безопасность: доступна");
        }
        catch
        {
            Console.WriteLine($"  Безопасность: недоступна");
        }
    }

    public void SetReadOnly(string path, bool readOnly)
    {
        FileInfo info = new FileInfo(path);
        info.IsReadOnly = readOnly;
        Console.WriteLine($"Файл {path} теперь {(readOnly ? "только для чтения" : "доступен для записи")}");
    }

    public void TryWriteToReadOnly(string path, string content)
    {
        try
        {
            File.WriteAllText(path, content);
            Console.WriteLine($"Запись выполнена: {path}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Ошибка записи (доступ запрещен): {ex.Message}");
        }
    }
}