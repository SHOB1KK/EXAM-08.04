using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class UpdateCarDto
{
    public int Id { get; set; }
    [MaxLength(100)] [Required]
    public string Model { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; } = true;
    public int BookingCount { get; set; }
}