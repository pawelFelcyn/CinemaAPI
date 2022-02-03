namespace Application.Dtos;

public record ReservationDto
{
    public int Id { get; init; }
    public int ShowingId { get; init; }
    public DateTime ShowingStarts { get; init; }
    public int FullPriceTicketsAmount { get; init; }
    public int HalfPriceTicketsAmount { get; init; }
    public string? ReservedByFirstName { get; init; }
    public string? ReservedByLastName { get; init; }
    public string? MovieTitle { get; init; }
}
