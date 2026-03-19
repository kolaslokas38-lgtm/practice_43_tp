using System;
using System.Collections.Generic;

class Person
{
    public string Name { get; }
    public string City { get; }
    public int Age { get; }

    public Person(string name, string city, int age)
    {
        Name = name;
        City = city;
        Age = age;
    }
}

class Program
{
    static void Main()
    {
        var people = new Person[]
        {
            new Person("Мирослав", "Гродно", 25),
            new Person("Ярик", "Минск", 30),
            new Person("Роман", "Брест", 28),
            new Person("Богдан", "Гомель", 22),
            new Person("Олег", "Могилев", 35),
            new Person("Руслан", "Витебск", 27)
        };

        Console.WriteLine("Список людей:");
        foreach (var person in people)
        {
            Console.WriteLine($"  {person.Name}, {person.City}, {person.Age} лет");
        }

        Console.WriteLine("Количество людей в каждом городе:");

        var cityCounts = new Dictionary<string, int>();

        foreach (var person in people)
        {
            if (cityCounts.ContainsKey(person.City))
                cityCounts[person.City]++;
            else
                cityCounts[person.City] = 1;
        }

        foreach (var pair in cityCounts)
        {
            Console.WriteLine($"  {pair.Key}: {pair.Value} чел.");
        }
    }
}