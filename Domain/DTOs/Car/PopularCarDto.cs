using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class PopularCarDto
{
    [MaxLength(100)] [Required]
    public string Model { get; set; }
    public int BookingCount { get; set; }
}