using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        IReport<string> stringReport = new TextReport<string>();
        ReportManager<string> stringManager = new ReportManager<string>(stringReport);

        stringManager.DisplayReport("Продажи за март: 1500 шт.");
        stringManager.SaveReport(stringManager.CreateReport("Тестовые данные"), "report.txt");

        Console.WriteLine();

        IReport<int> intReport = new TextReport<int>();
        ReportManager<int> intManager = new ReportManager<int>(intReport);

        intManager.DisplayReport(12345);

        Console.WriteLine();

        IReport<int[]> arrayReport = new TextReport<int[]>();
        ReportManager<int[]> arrayManager = new ReportManager<int[]>(arrayReport);

        int[] sales = [100, 250, 180, 320, 290];
        arrayManager.DisplayReport(sales);
    }
}