using System.ComponentModel.DataAnnotations;

namespace movies_backend.DTOs;

public class AddNewUserRoleDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}