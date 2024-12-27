using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movies_backend.Model;

[Table("actors")]
public class Actor
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public int GenderId { get; set; }
    [Required]
    public DateOnly BirthDate { get; set; }
    public DateOnly DeathDate { get; set; }
    [Required]
    [MaxLength(1000)]
    public string Biography { get; set; } = null!;

    [ForeignKey(nameof(GenderId))]
    public Gender Gender { get; set; } = null!;
}