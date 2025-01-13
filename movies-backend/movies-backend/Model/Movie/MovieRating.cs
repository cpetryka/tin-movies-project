using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace movies_backend.Model;

[Table("movieRatings")]
public class MovieRating
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int MovieId { get; set; }
    [Required]
    public int RatingId { get; set; }
    public int? UserId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public Movie Movie { get; set; } = null!;
    [ForeignKey(nameof(RatingId))]
    public Rating Rating { get; set; } = null!;
    [ForeignKey(nameof(UserId))]
    public User.User User { get; set; } = null!;
}