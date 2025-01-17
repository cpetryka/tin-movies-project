using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movies_backend.Model;

[Table("genres")]
public class Genre
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = null!;

    public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
}