using System;
using System.IO;
using System.Collections.Generic;

namespace Task;

public class CarFileReader
{
    private string filePath;

    public CarFileReader(string filePath)
    {
        this.filePath = filePath;
    }

    public List<Car> ReadCars()
    {
        List<Car> cars = new List<Car>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Файл не найден: {filePath}");
            return cars;
        }

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');

                if (parts.Length == 2)
                {
                    string brand = parts[0];
                    int year = int.Parse(parts[1]);

                    cars.Add(new Car(brand, year));
                }
            }
        }

        Console.WriteLine($"Загружено {cars.Count} автомобилей");
        return cars;
    }
}