Console.Write("Введите трехзначное число: ");
int chislo = int.Parse(Console.ReadLine()!);

int a = chislo / 10 % 10;   
int b = chislo % 10;

int otvet = a * b;

Console.WriteLine($"Произведение второй и последней цифр: {otvet}");