using System;

namespace Task4;

class Program
{
    static void Main()
    {
        var cars = new RentalCar[]
        {
            new RentalCar("Toyota", "Camry", 2020, 2500m, true),
            new RentalCar("BMW", "X5", 2021, 5000m, false),
            new RentalCar("Toyota", "Corolla", 2019, 2000m, true),
            new RentalCar("Audi", "A6", 2022, 4500m, true),
            new RentalCar("BMW", "3 Series", 2020, 3500m, false)
        };

        var service = new RentalService(cars);

        Console.WriteLine("Доступные авто:");
        foreach (var car in service.GetAvailableCars())
        {
            Console.WriteLine($"  {car.Brand} {car.Model}");
        }

        Console.WriteLine("Авто Toyota:");
        foreach (var car in service.GetCarsByBrand("Toyota"))
        {
            Console.WriteLine($"  {car.Brand} {car.Model}");
        }

        Console.WriteLine("Авто BMW:");
        foreach (var car in service.GetCarsByBrand("BMW"))
        {
            Console.WriteLine($"{car.Brand} {car.Model}");
        }
    }
}