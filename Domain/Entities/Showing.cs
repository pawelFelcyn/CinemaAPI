namespace Domain.Entities;

public class Showing
{
    public int Id { get; set; }
    public DateTime Starts { get; set; }
    public int TicketsAmount { get; set; }
    public int CinemaId { get; set; }
    public virtual Cinema? Cinema { get; set; }
    public int MovieId { get; set; }
    public virtual Movie? Movie { get; set; }
    public int CreatedById { get; set; }
    public virtual User? CreatedBy { get; set; }
}
