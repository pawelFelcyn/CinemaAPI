namespace Application.Dtos;

public record MovieDto
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public TimeSpan? Duration { get; init; }
}
