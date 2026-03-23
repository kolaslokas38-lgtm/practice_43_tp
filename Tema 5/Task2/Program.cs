using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        Actor[] actors1 =
        [
            new Actor("Швед", "Руслан"),
            new Actor("Петров", "Егор"),
            new Actor("Седеневский", "Мирослав")
        ];

        Actor[] actors2 =
        [
            new Actor("Макарчук", "Богдан"),
            new Actor("Петров", "Роман")
        ];

        Theater[] theaters =
        [
            new Theater("Большой театр", actors1, 500, 450),
            new Theater("Малый театр", actors2, 300, 280)
        ];

        for (int i = 0; i < theaters.Length; i++)
        {
            theaters[i].PerformPlay();
        }
    }
}