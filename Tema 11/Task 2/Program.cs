using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        IDocument doc = new PlainDocument("Содержание документа:\n- Введение\n- Основная часть\n- Заключение");

        Console.WriteLine("Базовый документ:");
        Console.WriteLine(doc.GetFormattedText());

        Console.WriteLine("\nПрименение декораторов");

        IDocument withHeader = new HeaderDecorator(doc, "Отчет о работе");
        Console.WriteLine("С заголовком:");
        Console.WriteLine(withHeader.GetFormattedText());

        Console.WriteLine();

        IDocument withFooter = new FooterDecorator(doc, "Конфиденциально");
        Console.WriteLine("С подвалом:");
        Console.WriteLine(withFooter.GetFormattedText());

        Console.WriteLine();

        IDocument withNumbers = new PageNumberDecorator(doc, 1);
        Console.WriteLine("С нумерацией:");
        Console.WriteLine(withNumbers.GetFormattedText());

        Console.WriteLine();

        IDocument fullDoc = new HeaderDecorator(
            new FooterDecorator(
                new PageNumberDecorator(doc, 1),
                "Конфиденциально"
            ),
            "Отчет о работе"
        );

        Console.WriteLine("Полное форматирование (заголовок + подвал + нумерация):");
        Console.WriteLine(fullDoc.GetFormattedText());
    }
}