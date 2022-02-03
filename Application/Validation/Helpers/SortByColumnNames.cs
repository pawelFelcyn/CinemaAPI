using Application.Query;
using Domain.Entities;
using Domain.Static;

namespace Application.Validation;

internal static class SortByColumnNames
{
    private static IEnumerable<string> Cinemas { get => SortByColumnsSelectors.ForCinemas.Keys; }
    private static IEnumerable<string> Movies { get => SortByColumnsSelectors.ForMovies.Keys; }
    private static IEnumerable<string> Showings { get => SortByColumnsSelectors.ForShowings.Keys; }
    private static IEnumerable<string> Reservations { get => SortByColumnsSelectors.ForReservations.Keys; }

    public static Dictionary<Type, string[]> SortByTypesSelector = new()
    {
        { typeof(ResourceQuery<Cinema>), Cinemas.ToArray() },
        { typeof(ResourceQuery<Movie>), Movies.ToArray() },
        { typeof(ResourceQuery<Showing>), Showings.ToArray() },
        { typeof(ResourceQuery<Reservation>), Reservations.ToArray() }
    };
}
