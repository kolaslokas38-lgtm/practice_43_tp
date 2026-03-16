Console.Write("Введите значение угла альфа в градусах: ");
double alpha = double.Parse(Console.ReadLine());

double radian = alpha * Math.PI / 180;

double numerator = Math.Sin(2 * radian) + Math.Sin(5 * radian) - Math.Sin(3 * radian);
double denominator = Math.Cos(radian) - Math.Cos(3 * radian) + Math.Cos(5 * radian);
double z1 = numerator / denominator;

double z2 = Math.Tan(3 * radian);

Console.WriteLine($"альфа = {alpha:F4}");
Console.WriteLine($"z1 = {z1:F4}");
Console.WriteLine($"z2 = {z2:F4}");

if (Math.Abs(z1 - z2) < 0.0001)
{
    Console.WriteLine("Результаты совпадают (в пределах погрешности)");
}
else
{
    Console.WriteLine("Результаты различаются");
}