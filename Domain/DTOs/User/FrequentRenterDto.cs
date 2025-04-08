using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class FrequentRenterDto
{
    [MaxLength(100)] [Required]
    public string UserName { get; set; }
    public int BookingCount { get; set; }
}