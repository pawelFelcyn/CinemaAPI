namespace Domain.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
}

public class InvalidEmailException :  BadRequestException
{
    public InvalidEmailException() : base("Invalid email")
    {
    }
}

public class InvalidPasswordException : BadRequestException
{
    public InvalidPasswordException() : base("Invalid password")
    {
    }
}

public class TooFewTicketsException : BadRequestException
{
    public TooFewTicketsException() : base("There are too few tickets availible for this showing")
    {
    }
}