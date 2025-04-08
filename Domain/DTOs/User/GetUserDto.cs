using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class GetUserDto
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [MaxLength(20)]
    public string Phone { get; set; }
}