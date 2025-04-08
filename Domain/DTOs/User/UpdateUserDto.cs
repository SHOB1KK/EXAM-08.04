using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.DTOs;

public class UpdateUserDto
{
    public string UserName { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [MaxLength(20)]
    public string Phone { get; set; }
}