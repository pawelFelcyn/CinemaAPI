namespace Domain.Exceptions;

public class NotFoundExcpetion : Exception
{
    public NotFoundExcpetion(string message) : base(message)
    {
    }
}

public class CinemaNotFoundException : NotFoundExcpetion
{
    public CinemaNotFoundException() : base("Cinema with given id was not found in database")
    {
    }
}

public class MovieNotFoundException : NotFoundExcpetion
{
    public MovieNotFoundException() : base("Movie with given id was not found in databse")
    {
    }
}

public class ShowingNotFoundException : NotFoundExcpetion
{
    public ShowingNotFoundException() : base("Showing was not found in database") 
    {
    }
}
