using System;

namespace Task4;

public partial class RentalCar
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; }

    public RentalCar(string brand, string model, int year, decimal pricePerDay, bool isAvailable)
    {
        Brand = brand;
        Model = model;
        Year = year;
        PricePerDay = pricePerDay;
        IsAvailable = isAvailable;
    }

    public void Rent()
    {
        IsAvailable = false;
    }

    public void Return()
    {
        IsAvailable = true;
    }
}