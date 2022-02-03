using Domain.Entities;
using Domain.Models;
using Domain.Static;

namespace Infrastructure.Pagination;

internal class CinemaSorter : ISorter<Cinema>
{
    public IQueryable<Cinema> Sort(IQueryable<Cinema> collection, SortDirection sortDirection, string sortBy)
    {
        return sortDirection == SortDirection.Asc ? collection.OrderBy(SortByColumnsSelectors.ForCinemas[sortBy])
            : collection.OrderByDescending(SortByColumnsSelectors.ForCinemas[sortBy]);
    }
}
