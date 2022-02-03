using Domain.Entities;
using Domain.Models;

namespace Infrastructure.Pagination;

public class PageCreator<T> : IPageCreator<T>
{
    private readonly ISorter<T> _sorter;
    private readonly IFilter<T> _filter;

    public PageCreator(ISorter<T> sorter, IFilter<T> filter)
    {
        _sorter = sorter;
        _filter = filter;
    }

    public Page<T> CreatePage(IQueryable<T> collection, ResourceQuery query)
    {
        var filtered = _filter.Filter(collection, query.SearchPhrase);
        var sorted = _sorter.Sort(filtered, query.SortDirection, query.SortBy);
        
        var skip = (query.PageNumber - 1) * query.PageSize;

        var ultimateCollection = sorted.Skip(skip).Take(query.PageSize).AsEnumerable();

        return new Page<T>(ultimateCollection, query.PageSize, query.PageNumber, sorted.Count());
    }
}
