using System;
using Task3;

public class WeatherStation
{
    private double temperature;
    private double windSpeed;

    public event WeatherChanged? WeatherChanged;

    public void UpdateWeather(double temperature, double wind)
    {
        temperature = temperature;
        windSpeed = wind;

        WeatherChanged?.Invoke(temperature, windSpeed);
    }
}