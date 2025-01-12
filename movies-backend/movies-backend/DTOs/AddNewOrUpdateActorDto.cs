using System.ComponentModel.DataAnnotations;

namespace movies_backend.DTOs;

public class AddNewOrUpdateActorDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public int GenderId { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    public DateTime DeathDate { get; set; }
    [Required]
    [MaxLength(1000)]
    public string Biography { get; set; } = null!;
}