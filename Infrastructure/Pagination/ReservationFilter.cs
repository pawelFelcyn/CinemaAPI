using Domain.Entities;

namespace Infrastructure.Pagination;

internal class ReservationFilter : IFilter<Reservation>
{
    public IQueryable<Reservation> Filter(IQueryable<Reservation> collection, string searchPhrase)
    {
        return collection.Where(r => r.Showing.Movie.Title.ToLower().Contains(searchPhrase) || searchPhrase.Contains(r.Showing.Movie.Title.ToLower()));
    }
}
