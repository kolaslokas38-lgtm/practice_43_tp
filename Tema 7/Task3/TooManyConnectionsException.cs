using System;

namespace Task;

public class TooManyConnectionsException : Exception
{
    public TooManyConnectionsException() { }

    public TooManyConnectionsException(string message) : base(message) { }

    public TooManyConnectionsException(string message, Exception inner)
        : base(message, inner) { }
}