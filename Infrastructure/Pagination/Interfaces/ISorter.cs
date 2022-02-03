using Domain.Models;

namespace Infrastructure.Pagination;

public interface ISorter<T>
{
    IQueryable<T> Sort(IQueryable<T> collection, SortDirection sortDirection, string sortBy);
}
