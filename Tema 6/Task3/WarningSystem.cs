using System;
using System.Data;

namespace Task3;

public class WarningSystem
{
    public void WeatherChanged(double temperature, double windSpeed)
    {
        if (windSpeed > 20)
        {
            Console.WriteLine($"Внимание скорость ветра достигло:{windSpeed}м/c");
        }
        else if (temperature > 35)
        {
            Console.WriteLine($"Внимание температура достигла значение:{temperature}");
        }
        else
        {
            Console.WriteLine("Погода отличная!");
        }
    }
}
