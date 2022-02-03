namespace Application.Dtos;

public class MovieDetailsDto
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Director { get; init; }
    public string? Description { get; init; }
    public DateTime? DateOfRelease { get; init; }
    public TimeSpan? Duration { get; init; }
}