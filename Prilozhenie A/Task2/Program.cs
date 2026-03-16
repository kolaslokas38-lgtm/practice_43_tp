Console.WriteLine("Введите четырехзначное число");
int chislo = int.Parse(Console.ReadLine());

int a = chislo / 100;
int b = chislo % 100;
    
int c = b * 100 + a;

Console.WriteLine($"Результат:{c}");
Console.ReadLine();
