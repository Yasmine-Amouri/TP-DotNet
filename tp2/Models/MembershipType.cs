using System.ComponentModel.DataAnnotations;

namespace tp2.Models;

public class MembershipType
{
    public int Id { get; set; }
    [Required, StringLength(50)]
    public string Name { get; set; } = "";
    public int DiscountPercent { get; set; }
}