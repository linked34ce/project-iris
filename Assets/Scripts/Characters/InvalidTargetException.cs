using System;

public class InvalidTargetException : Exception
{
    public InvalidTargetException() { }

    public InvalidTargetException(string message) : base(message) { }

    public InvalidTargetException(string message, Exception inner) : base(message, inner) { }
}
