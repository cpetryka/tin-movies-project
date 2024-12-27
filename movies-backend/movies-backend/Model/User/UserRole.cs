using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movies_backend.Model.User;

[Table("userRoles")]
public class UserRole
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}