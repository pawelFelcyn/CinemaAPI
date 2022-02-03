namespace Domain.Models;

public class Page<T>
{
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
    public int TotalPagesAmount { get; init; }
    public int ItemsFrom { get; init; }
    public int ItemsTo { get; init; }
    public IEnumerable<T> Items { get; init; }

    public Page(IEnumerable<T> items, int pageSize, int pageNumber, int totalItemsAmount)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        Items = items;
        ItemsFrom = (pageNumber - 1) * pageSize + 1;
        ItemsTo = ItemsFrom + pageSize - 1;
        TotalPagesAmount = (int)Math.Ceiling((decimal)totalItemsAmount / (decimal)pageSize);
    }

    public Page()
    {
    }
}
