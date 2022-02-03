namespace Application.Dtos;

public record CinemaDetailsDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? ContactEmail { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Description { get; init; }
    public string? City { get; init; }
    public string? Street { get; init; }
    public string? PostalCode { get; init; }
}
