using movies_backend.Model;

namespace movies_backend.DTOs;

public class GetActorDto
{
    public string Name { get; set; } = null!;
    public string GenderName { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public DateOnly DeathDate { get; set; }
    public string Biography { get; set; } = null!;
}