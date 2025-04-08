using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Booking
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int CarId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public decimal TotalPrice { get; set; }

    //navi
    public User User { get; set; }
    public Car Car { get; set; }
}
