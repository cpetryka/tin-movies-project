using movies_backend.Model;

namespace movies_backend.DTOs;

public class GetActorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int GenderId { get; set; }
    public string GenderName { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public DateOnly DeathDate { get; set; }
    public string Biography { get; set; } = null!;
}