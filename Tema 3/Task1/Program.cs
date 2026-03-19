using System;

namespace LabWork12
{
    class A
    {
        public int a;
        public int b;

        public A(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        public double Method1()
        {
            return 1.0 / a + 1.0 / Math.Sqrt(b);
        }

        public double Method2()
        {
            return Math.Pow(a, 6);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A obj = new A(2, 9);

            Console.WriteLine($"a = {obj.a}, b = {obj.b}");

            Console.WriteLine($"1/a + 1/√b = {obj.Method1()}");
            Console.WriteLine($"a⁶ = {obj.Method2()}");

            Console.ReadKey();
        }
    }
}