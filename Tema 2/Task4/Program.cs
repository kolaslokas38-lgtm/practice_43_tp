using System;

class Program
{
    static void Main()
    {
        int n = 20;
        double[,] salary = new double[n, 12];
        Random r = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                salary[i, j] = r.Next(30000, 100001);
            }
        }

        double febSum = 0;
        double octSum = 0;

        for (int i = 0; i < n; i++)
        {
            febSum += salary[i, 1];  
            octSum += salary[i, 9];  
        }

        Console.WriteLine($"Февраль: {febSum:F2}");
        Console.WriteLine($"Октябрь: {octSum:F2}");

        if (febSum < octSum)
        {
            Console.WriteLine("Верно: февраль < октябрь");
        }
        else
        {
            Console.WriteLine("Неверно: февраль >= октябрь");
        }
    }
}