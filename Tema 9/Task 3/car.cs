namespace Task;

public class Car
{
    public string Brand { get; set; }
    public int Year { get; set; }

    public Car(string brand, int year)
    {
        Brand = brand;
        Year = year;
    }

    public override string ToString()
    {
        return $"{Brand} ({Year})";
    }
}