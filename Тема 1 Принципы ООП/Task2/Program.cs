using System;

public class Program
{
    public static void Main()
    {
        int chislo = 357;
        
        int pervayacifra = chislo / 100;
        int vtorayacifra = (chislo / 10) % 10;
        int tretyacifra = chislo % 10;

        bool vozrastaet = (pervayacifra < vtorayacifra) && (vtorayacifra < tretyacifra);

        bool ybivaet = (pervayacifra > vtorayacifra) && (vtorayacifra > tretyacifra);

        bool vozrastaetybivaet = vozrastaet || ybivaet;

        Console.WriteLine($"Число: {chislo}");
        Console.WriteLine($"Цифры: {pervayacifra}, {vtorayacifra}, {tretyacifra}");
        Console.WriteLine($"Возрастает: {vozrastaet}");
        Console.WriteLine($"Убывает: {ybivaet}");
        Console.WriteLine($"Образует возрастающую или убывающую последовательность: {vozrastaetybivaet}");
    }
}