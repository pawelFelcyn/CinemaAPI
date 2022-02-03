namespace Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int HalfPriceTicketsAmount { get; set; }
    public int FullPriceTicketsAmount { get; set; }
    public int ReservedById { get; set; }
    public virtual User? ReservedBy { get; set; }
    public int ShowingId { get; set; }
    public virtual Showing? Showing { get; set; }
}
