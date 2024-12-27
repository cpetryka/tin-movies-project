using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movies_backend.Model;

[Table("actorRoles")]
public class ActorRole
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = null!;
}