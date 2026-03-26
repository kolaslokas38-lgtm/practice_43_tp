using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        CircularBufferManager<int> manager = new CircularBufferManager<int>(3);

        manager.AddItem(1);
        manager.AddItem(2);
        manager.AddItem(3);
        manager.ShowAll();
        manager.ShowStatus();

        manager.AddItem(4);
        manager.ShowAll();

        manager.ShowFirst();

        manager.RemoveLast();
        manager.ShowAll();

        manager.RemoveLast();
        manager.RemoveLast();
        manager.RemoveLast();

        manager.ShowAll();
    }
}