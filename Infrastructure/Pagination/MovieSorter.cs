using Domain.Entities;
using Domain.Models;
using Domain.Static;

namespace Infrastructure.Pagination;

internal class MovieSorter : ISorter<Movie>
{
    public IQueryable<Movie> Sort(IQueryable<Movie> collection, SortDirection sortDirection, string sortBy)
    {
        return sortDirection == SortDirection.Asc ? collection.OrderBy(SortByColumnsSelectors.ForMovies[sortBy])
            : collection.OrderByDescending(SortByColumnsSelectors.ForMovies[sortBy]);
    }
}
