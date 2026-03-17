using System;

public class Program
{
    public static void Main()
    {
        int m = 2; 
        int k = 12; 

        string suit = "";
        string rank = "";

        switch (m)
        {
            case 1:
                suit = "пик";
                break;
            case 2:
                suit = "треф";
                break;
            case 3:
                suit = "бубен";
                break;
            case 4:
                suit = "червей";
                break;
            default:
                suit = "неизвестная масть";
                break;
        }

        switch (k)
        {
            case 6:
                rank = "шестерка";
                break;
            case 7:
                rank = "семерка";
                break;
            case 8:
                rank = "восьмерка";
                break;
            case 9:
                rank = "девятка";
                break;
            case 10:
                rank = "десятка";
                break;
            case 11:
                rank = "валет";
                break;
            case 12:
                rank = "дама";
                break;
            case 13:
                rank = "король";
                break;
            case 14:
                rank = "туз";
                break;
            default:
                rank = "неизвестное достоинство";
                break;
        }

        Console.WriteLine($"Масть (m = {m}): {suit}");
        Console.WriteLine($"Достоинство (k = {k}): {rank}");
        Console.WriteLine($"Карта: {rank} {suit}");
    }
}
