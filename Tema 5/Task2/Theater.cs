using System;

namespace Task;

public class Theater
{
    public string Name { get; set; }

    private Actor[] actors;

    private Stage stage;

    private Audience audience;

    public Theater(string name, Actor[] actors, int stageCapacity, int audienceCount)
    {
        Name = name;
        this.actors = actors;

        stage = new Stage("Главная сцена", stageCapacity);

        audience = new Audience(audienceCount);
    }

    public void PerformPlay()
    {
        Console.WriteLine($"Театр {Name} начинает представление");

        stage.Prepare();

        Console.WriteLine("На сцену выходят актеры:");

        for (int i = 0; i < actors.Length; i++)
        {
            actors[i].Perform();
        }

        audience.Watch();
        audience.Clap();

        Console.WriteLine("Спектакль окончен!");
    }
}