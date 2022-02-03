namespace Infrastructure.Pagination;

public interface IFilter<T>
{
    IQueryable<T> Filter (IQueryable<T> collection, string searchPhrase);
}
