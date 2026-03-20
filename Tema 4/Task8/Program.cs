using System;

namespace Task;

public class Program
{
    public static void AddComplex(
        in double real1,
        in double imag1,
        in double real2,
        in double imag2,
        out (double real, double imag) result)
    {
        result = (real1 + real2, imag1 + imag2);
    }

    public static void AddComplex(
        in (double real, double imag) c1,
        in (double real, double imag) c2,
        out (double real, double imag) result)
    {
        result = (c1.real + c2.real, c1.imag + c2.imag);
    }

    public static void Main()
    {
        AddComplex(1.0, 2.0, 3.0, 4.0, out var sum1);
        Console.WriteLine($"({sum1.real}, {sum1.imag})");

        AddComplex((1.0, 2.0), (3.0, 4.0), out var sum2);
        Console.WriteLine($"({sum2.real}, {sum2.imag})");
    }
}