using System.ComponentModel.DataAnnotations;

namespace movies_backend.DTOs;

public class AddNewUserDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Email { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Password { get; set; } = null!;
    [Required]
    public int UserRoleId { get; set; }
}