using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [MaxLength(20)]
    public string Phone { get; set; }

    // navi
    public List<Booking> Bookings { get; set; } = new();
}
