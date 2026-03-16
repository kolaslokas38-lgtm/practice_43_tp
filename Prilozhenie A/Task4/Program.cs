    Console.Write("Введите длину первого основания a: ");
    double a = double.Parse(Console.ReadLine()!);

    Console.Write("Введите длину второго основания b: ");
    double b = double.Parse(Console.ReadLine()!);

    Console.Write("Введите высоту h: ");
    double h = double.Parse(Console.ReadLine()!);

    Console.WriteLine("Вычисление площади:");
    double s = (a + b) / 2 * h;

    Console.WriteLine($"Площадь трапеции S = {s}");