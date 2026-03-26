using System;

namespace Task;

public class CircularBufferManager<T>
{
    private MyCircularBuffer<T> buffer;

    public CircularBufferManager(int capacity)
    {
        buffer = new MyCircularBuffer<T>(capacity);
    }

    public void AddItem(T item)
    {
        buffer.Add(item);
        Console.WriteLine($"Добавлено: {item}");
    }

    public void RemoveLast()
    {
        if (buffer.IsEmpty)
        {
            Console.WriteLine("Буфер пуст, удаление невозможно");
            return;
        }

        T item = buffer.Remove();
        Console.WriteLine($"Удалено: {item}");
    }

    public void ShowFirst()
    {
        if (buffer.IsEmpty)
        {
            Console.WriteLine("Буфер пуст");
            return;
        }

        Console.WriteLine($"Первый элемент: {buffer.Peek()}");
    }

    public void ShowAll()
    {
        if (buffer.IsEmpty)
        {
            Console.WriteLine("Буфер пуст");
            return;
        }

        T[] items = buffer.ToArray();
        Console.Write("Буфер: ");

        for (int i = 0; i < items.Length; i++)
        {
            Console.Write($"{items[i]} ");
        }

        Console.WriteLine();
    }

    public void ShowStatus()
    {
        Console.WriteLine($"Элементов: {buffer.Count}, Емкость: {buffer.Capacity}");
    }
}