using System;
using System.Collections.Generic;

namespace Task;

public class Program
{
    public static void Main()
    {
        string filePath = @"C:\Users\MSI\Documents\Tema 9\Task 3\bin\Debug\net8.0";

        List<Car> cars = new List<Car>
        {
            new Car("Toyota", 2015),
            new Car("BMW", 2020),
            new Car("Lada", 2010),
            new Car("Mercedes", 2022),
            new Car("Honda", 2018),
            new Car("Audi", 2023)
        };

        CarFileWriter writer = new CarFileWriter(filePath);
        writer.WriteCars(cars);

        CarFileReader reader = new CarFileReader(filePath);
        List<Car> loadedCars = reader.ReadCars();


        CarProcessor processor = new CarProcessor();
        processor.PrintCars(loadedCars, "Все автомобили");

        List<Car> newCars = processor.FilterByYear(loadedCars, 2020);
        processor.PrintCars(newCars, "Автомобили с 2020 года");

        List<Car> oldCars = processor.FilterByYear(loadedCars, 2015);
        processor.PrintCars(oldCars, "Автомобили с 2015 года");
    }
}