namespace Application.Dtos;

public record ShowingDetailsDto
{
    public int Id { get; init; }
    public DateTime Starts { get; init; }
    public int TicketsAmount { get; set; }
    public string? MovieTitle { get; init; }
    public string? MovieDirector { get; init; }
    public string? MovieDescription { get; init; }
    public TimeSpan MovieDuration { get; init; }
    public string? CinemaName { get; init; }
}
