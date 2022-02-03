using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Static;

public static class SortByColumnsSelectors
{
    public static Dictionary<string, Expression<Func<Cinema, object>>> ForCinemas 
    {
        get => new()
        {
            { "name", c => c.Name },
            { "id", c => c.Id },
            { "", c => c.Id}
        };
    }

    public static Dictionary<string, Expression<Func<Movie, object>>> ForMovies 
    { 
        get => new()
        {
            { "title", m => m.Title },
            { "director", m => m.Director },
            { "id", m => m.Id },
            { "", m => m.Id },
            { "duration", m => m.Duration }
        };
    }

    public static Dictionary <string, Expression<Func<Showing, object>>> ForShowings
    {
        get => new()
        {
            { "starts", s => s.Starts },
            { "title", s => s.Movie.Title },
            { "id", s => s.Id },
            { "", s => s.Id }
        };
    }

    public static Dictionary<string, Expression<Func<Reservation, object>>> ForReservations
    {
        get => new()
        {
            { "movietitle", r => r.Showing.Movie.Title },
            { "id", r => r.Id },
            { "", r => r.Id }
        };
    }
}
