using System;
using System.IO;
using System.Collections.Generic;

namespace Task;

public class CarFileWriter
{
    private string filePath;

    public CarFileWriter(string filePath)
    {
        this.filePath = filePath;
    }

    public void WriteCars(List<Car> cars)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Car car in cars)
            {
                writer.WriteLine($"{car.Brand}|{car.Year}");
            }
        }

        Console.WriteLine($"Сохранено {cars.Count} автомобилей");
    }
}