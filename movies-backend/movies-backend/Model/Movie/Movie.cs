using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movies_backend.Model;

[Table("movies")]
public class Movie
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Director { get; set; } = null!;
    [Required]
    public DateOnly ReleaseDate { get; set; }
    [Required]
    public int Duration { get; set; }
    public string? TmdbId { get; set; } = null!;

    public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<MovieActor> Actors { get; set; } = new HashSet<MovieActor>();
}