using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class CarBookingDetailsDto
{
    [MaxLength(100)] [Required]
    public string UserName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
}