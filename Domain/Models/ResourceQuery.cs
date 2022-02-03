namespace Domain.Models;

public class ResourceQuery
{
    public int PageSize { get; init; } = 10;
    public int PageNumber { get; init; } = 1;
    private string searchPhrease = string.Empty;
    public string SearchPhrase
    {
        get => searchPhrease;
        init
        {
            searchPhrease = value.ToLower();
        }
    }
    private string? sortBy = string.Empty;
    public string? SortBy 
    { 
        get => sortBy; 
        init
        {
            sortBy = value?.ToLower();
        }
    }
    public SortDirection SortDirection { get; init; } = SortDirection.Asc;
}
