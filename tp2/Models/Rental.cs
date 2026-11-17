namespace tp2.Models;

public class Rental
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentedAt { get; set; }
}