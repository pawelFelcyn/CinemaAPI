namespace Application.Dtos;

public record ShowingDto
{
    public int Id { get; init; }
    public string? MovieTitle { get; init; }
    public DateTime Starts { get; init; }
    public int TicketsAmount { get; set; }
}
