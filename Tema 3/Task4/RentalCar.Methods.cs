using System;
using System.Collections.Generic;

namespace Task4;

class RentalService
{
    private RentalCar[] _cars;

    public RentalService(RentalCar[] cars)
    {
        _cars = cars;
    }

    public RentalCar[] GetAvailableCars()
    {
        var result = new List<RentalCar>();

        foreach (var car in _cars)
        {
            if (car.IsAvailable)
                result.Add(car);
        }

        return result.ToArray();
    }

    public RentalCar[] GetCarsByBrand(string brand)
    {
        var result = new List<RentalCar>();

        foreach (var car in _cars)
        {
            if (car.Brand == brand)
                result.Add(car);
        }

        return result.ToArray();
    }
}