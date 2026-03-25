using System;

namespace Task;

public class InvalidGradeException : Exception
{
    public InvalidGradeException() { }

    public InvalidGradeException(string message) : base(message) { }

    public InvalidGradeException(string message, Exception inner)
        : base(message, inner) { }
}