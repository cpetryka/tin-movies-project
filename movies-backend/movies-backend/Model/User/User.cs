using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movies_backend.Model.User;

[Table("users")]
public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public int UserRoleId { get; set; }


    [ForeignKey(nameof(UserRoleId))]
    public UserRole UserRole { get; set; } = null!;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}