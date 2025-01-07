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
    [Required]
    public string TmdbId { get; set; } = null!;
    [Required]
    public string PosterUrl { get; set; } = null!;

    public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
    public ICollection<MovieActor> Actors { get; set; } = new HashSet<MovieActor>();
    public ICollection<MovieRating> MovieRatings { get; set; } = new HashSet<MovieRating>();

    public double GetAverageRating()
    {
        if (MovieRatings.Count == 0)
        {
            return 0;
        }

        double sum = 0;
        foreach (var movieRating in MovieRatings)
        {
            sum += movieRating.Rating.StarsNumber;
        }

        return sum / MovieRatings.Count;
    }
}