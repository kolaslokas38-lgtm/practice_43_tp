using System;
using Task3;

namespace Task;

public class Program
{
    public static void Main()
    {
        WeatherStation station = new WeatherStation();

        DisplayPanel display = new DisplayPanel();
        WarningSystem warning = new WarningSystem();

        station.WeatherChanged += display.OnWeatherChanged;
        station.WeatherChanged += warning.WeatherChanged;

        Console.WriteLine("Обновление погоды 1:");
        station.UpdateWeather(25, 10);

        Console.WriteLine("Обновление погоды 2 (шторм):");
        station.UpdateWeather(22, 25);

        Console.WriteLine("Обновление погоды 3 (жара):");
        station.UpdateWeather(38, 5);
    }
}