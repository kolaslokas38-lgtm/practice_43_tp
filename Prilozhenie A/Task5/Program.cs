Console.Write("Введите вес в фунтах и нажмите <Enter> - >:");
double fynt = double.Parse(Console.ReadLine()!);

double kilogrames = fynt * 409.5 / 1000;

Console.WriteLine($"{fynt} фунт(а/ов) — это {kilogrames} кг.");