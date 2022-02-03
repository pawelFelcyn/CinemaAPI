using Domain.Entities;

namespace Infrastructure.Pagination;

internal class MovieFilter : IFilter<Movie>
{
    public IQueryable<Movie> Filter(IQueryable<Movie> collection, string searchPhrase)
    {
        return collection.Where(m =>  searchPhrase == string.Empty || m.Title.ToLower().Contains(searchPhrase) || searchPhrase.Contains(m.Title.ToLower())
                                || m.Director.ToLower().Contains(searchPhrase) || searchPhrase.Contains(m.Director.ToLower()));
    }
}
