namespace Domain.Modification;

public record CinemaModificationParams
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? ContactEmail { get; init; }
    public string? PhoneNumber { get; init; }
}
