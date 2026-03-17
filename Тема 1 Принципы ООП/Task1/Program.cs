using System;

namespace Calculates
{
    public class Program
    {
        public static void Main()
        {
            Console.Write("Введите первое число: ");
            double pervoechislo = double.Parse(Console.ReadLine());

            Console.Write("Введите второе число: ");
            double vtoroechislo = double.Parse(Console.ReadLine());

            CalculateAndDisplayResults(pervoechislo, vtoroechislo);
        }

        private static void CalculateAndDisplayResults(double pervoechislo, double vtoroechislo)
        {
            double sum = pervoechislo + vtoroechislo;
            double difference = pervoechislo - vtoroechislo;
            double product = pervoechislo * vtoroechislo;
            double quotient = pervoechislo / vtoroechislo;

            Console.WriteLine($"Решение для чисел {pervoechislo} and {vtoroechislo}:");
            Console.WriteLine($"Сумма: {pervoechislo} + {vtoroechislo} = {sum}");
            Console.WriteLine($"Вычитание: {pervoechislo} - {vtoroechislo} = {difference}");
            Console.WriteLine($"Умножение: {pervoechislo} × {vtoroechislo} = {product}");
            Console.WriteLine($"Деление: {pervoechislo} ÷ {vtoroechislo} = {quotient}");
        }
    }
}

           