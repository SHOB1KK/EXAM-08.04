using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.DTOs;

public class UpdateBookingDto
{
    [MaxLength(50)]
    public string UserName { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(20)]
    public string Phone { get; set; }
}