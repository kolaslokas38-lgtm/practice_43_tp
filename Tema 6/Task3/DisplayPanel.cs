using System;

namespace Task3;

public class DisplayPanel
{
    public void OnWeatherChanged(double temperatue, double windSpeed)
    {
        Console.WriteLine($"Температура:{temperatue},Скорость ветра:{windSpeed}м/с");
    }
}