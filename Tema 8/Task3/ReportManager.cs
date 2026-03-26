using System;
using System.IO;

namespace Task;

public class ReportManager<T>
{
    private IReport<T> reportGenerator;

    public ReportManager(IReport<T> reportGenerator)
    {
        this.reportGenerator = reportGenerator;
    }

    public string CreateReport(T data)
    {
        return reportGenerator.Generate(data);
    }

    public void SaveReport(string report, string filename)
    {
        try
        {
            File.WriteAllText(filename, report);
            Console.WriteLine($"Отчет сохранен в файл: {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения: {ex.Message}");
        }
    }

    public void DisplayReport(T data)
    {
        string report = CreateReport(data);
        Console.WriteLine(report);
    }
}