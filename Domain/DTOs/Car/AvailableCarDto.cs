using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class AvailableCarDto
{
    [MaxLength(100)] [Required]
    public string Model { get; set; }
    public decimal PricePerDay { get; set; }
}