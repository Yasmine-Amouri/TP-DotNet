using System.ComponentModel.DataAnnotations;

namespace tp2.Models;

public class Customer
{
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string Name { get; set; } = "";
    [EmailAddress]
    public string? Email { get; set; }
    public int MembershipTypeId { get; set; }
    public MembershipType? MembershipType { get; set; }
    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}