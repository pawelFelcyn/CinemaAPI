using Domain.Entities;

namespace Infrastructure.Pagination;

internal class CinemaFilter : IFilter<Cinema>
{
    public IQueryable<Cinema> Filter(IQueryable<Cinema> collection, string searchPhrase)
    {
        return collection.Where(c => searchPhrase == string.Empty || c.Name.ToLower().Contains(searchPhrase) || searchPhrase.Contains(c.Name.ToLower()));
    }
}
