using System;
using System.Collections.Generic;

namespace Task;

public class CarProcessor
{
    public List<Car> FilterByYear(List<Car> cars, int minYear)
    {
        List<Car> result = new List<Car>();

        foreach (Car car in cars)
        {
            if (car.Year >= minYear)
            {
                result.Add(car);
            }
        }

        return result;
    }

    public void PrintCars(List<Car> cars, string title)
    {
        Console.WriteLine($"\n{title}:");

        if (cars.Count == 0)
        {
            Console.WriteLine("  Нет автомобилей");
            return;
        }

        foreach (Car car in cars)
        {
            Console.WriteLine($"  {car}");
        }
    }
}