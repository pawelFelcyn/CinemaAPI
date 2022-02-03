namespace Domain.Exceptions;

public class ForbidException : Exception
{
    public ForbidException(string message) : base(message)
    {
    }
}

public class CantUpdateCinemaException : ForbidException
{
    public CantUpdateCinemaException() : base("You are not authorized to update this cinema")
    {
    }
}

public class CantDeleteCinemaException : ForbidException
{
    public CantDeleteCinemaException() : base("You are not authorized to delete this cinema")
    {
    }
}

public class CantUpdateMovieException : ForbidException
{
    public CantUpdateMovieException() : base("You are not authorized to update this movie")
    {
    }
}

public class CantDeleteMovieException : ForbidException
{
    public CantDeleteMovieException() : base("You are not authorized to delete this movie")
    {
    }
}

public class CantCreateShowingException : ForbidException
{
    public CantCreateShowingException() : base("You are not authorized to create a showing in this cinema")
    {
    }
}

public class CantDeleteShowingException : ForbidException
{
    public CantDeleteShowingException() : base("You are not authorized to delete this showing")
    {
    }
}

public class CantGetReservationsException : ForbidException
{
    public CantGetReservationsException() : base("You are not authorized to get these reservations")
    {
    }
}