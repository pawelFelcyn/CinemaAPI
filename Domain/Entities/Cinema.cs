namespace Domain.Entities;

public class Cinema
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ContactEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime DateOfAppending { get; set; }
    public int CreatedById { get; set; }
    public virtual User? CreatedBy { get; set; }
    public int AddressId { get; set; }
    public virtual Address? Address { get; set; }

    public Cinema()
    {
        DateOfAppending = DateTime.UtcNow;
    }
}
