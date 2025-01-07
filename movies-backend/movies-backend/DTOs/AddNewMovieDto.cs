using System.ComponentModel.DataAnnotations;

namespace movies_backend.DTOs;

public class AddNewMovieDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Director { get; set; } = null!;
    [Required]
    public DateTime ReleaseDate { get; set; }
    [Required]
    public int Duration { get; set; }
    [Required]
    public string TmdbId { get; set; } = null!;
    [Required]
    public string PosterUrl { get; set; } = null!;

    public ICollection<ActorWithRoleDto> Actors { get; set; } = new List<ActorWithRoleDto>();
    public ICollection<string> Genres { get; set; } = new HashSet<string>();
}