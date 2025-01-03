using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movies_backend.Model;

[Table("ratings")]
public class Rating
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int StarsNumber { get; set; }

    public ICollection<MovieRating> MovieRatings { get; set; } = new HashSet<MovieRating>();
}