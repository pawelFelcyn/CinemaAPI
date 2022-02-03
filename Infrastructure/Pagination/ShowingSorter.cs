using Domain.Entities;
using Domain.Models;
using Domain.Static;

namespace Infrastructure.Pagination;

internal class ShowingSorter : ISorter<Showing>
{
    public IQueryable<Showing> Sort(IQueryable<Showing> collection, SortDirection sortDirection, string sortBy)
    {
        return sortDirection == SortDirection.Asc ? collection.OrderBy(SortByColumnsSelectors.ForShowings[sortBy])
            : collection.OrderByDescending(SortByColumnsSelectors.ForShowings[sortBy]);
    }
}
