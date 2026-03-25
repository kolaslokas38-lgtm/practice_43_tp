using System;

namespace Task;

public class SerializationOperationException : Exception
{
    public SerializationOperationException() { }

    public SerializationOperationException(string message) : base(message) { }

    public SerializationOperationException(string message, Exception inner)
        : base(message, inner) { }
}