using System;

namespace Task;

public class MyCircularBuffer<T>
{
    private T[] buffer;
    private int head;
    private int count;

    public int Capacity { get; }
    public int Count => count;
    public bool IsFull => count == Capacity;
    public bool IsEmpty => count == 0;

    public MyCircularBuffer(int capacity)
    {
        Capacity = capacity;
        buffer = new T[capacity];
        head = 0;
        count = 0;
    }

    public void Add(T item)
    {
        int index = (head + count) % Capacity;
        buffer[index] = item;

        if (count == Capacity)
        {
            head = (head + 1) % Capacity;
        }
        else
        {
            count++;
        }
    }

    public T Remove()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Буфер пуст");
        }

        T item = buffer[head];
        head = (head + 1) % Capacity;
        count--;

        return item;
    }

    public T Peek()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Буфер пуст");
        }

        return buffer[head];
    }

    public void Clear()
    {
        buffer = new T[Capacity];
        head = 0;
        count = 0;
    }

    public T[] ToArray()
    {
        T[] result = new T[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = buffer[(head + i) % Capacity];
        }

        return result;
    }
}