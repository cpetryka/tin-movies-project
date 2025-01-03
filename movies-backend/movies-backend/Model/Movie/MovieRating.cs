using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace movies_backend.Model;

[Table("movieRatings")]
public class MovieRating
{
    [Key]
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int RatingId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public Movie Movie { get; set; } = null!;
    [ForeignKey(nameof(RatingId))]
    public Rating Rating { get; set; } = null!;
}