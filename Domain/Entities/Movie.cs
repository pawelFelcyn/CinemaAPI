namespace Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Director { get; set; }
    public string? Description { get; set; }
    public DateTime? DateOfRelease { get; set; }
    public TimeSpan? Duration { get; set; }

    public int CreatedById { get; set; }
    public virtual User? CreatedBy { get; set; }
}
