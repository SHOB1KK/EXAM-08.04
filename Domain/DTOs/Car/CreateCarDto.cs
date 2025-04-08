using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class CreateCarDto
{
    public string Model { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; } = true;
    public int BookingCount { get; set; }
}