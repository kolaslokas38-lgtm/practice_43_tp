namespace Task;

public class Actor
{
    public string Name { get; set; }
    public string Role { get; set; }

    public Actor(string name, string role)
    {
        Name = name;
        Role = role;
    }

    public void Perform()
    {
        Console.WriteLine($"Актер {Name} играет роль {Role}");
    }
}