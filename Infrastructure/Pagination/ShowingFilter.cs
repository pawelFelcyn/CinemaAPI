using Domain.Entities;

namespace Infrastructure.Pagination;

internal class ShowingFilter : IFilter<Showing>
{
    public IQueryable<Showing> Filter(IQueryable<Showing> collection, string searchPhrase)
    {
        return collection.Where(s => s.Movie.Title.ToLower().Contains(searchPhrase) ||
                                 searchPhrase.Contains(s.Movie.Title.ToLower()) ||
                                 s.Movie.Director.ToLower().Contains(searchPhrase) ||
                                 searchPhrase.Contains(s.Movie.Director.ToLower()));
    }
}
