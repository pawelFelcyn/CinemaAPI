namespace Application.Dtos;

public record CinemaDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? ContactEmail { get; init; }
    public string? PhoneNumber { get; init; }
}
