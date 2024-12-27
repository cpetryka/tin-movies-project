using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace movies_backend.Model;

[Table("movieActors")]
[PrimaryKey(nameof(MovieId), nameof(ActorId))]
public class MovieActor
{
    public int MovieId { get; set; }
    public int ActorId { get; set; }
    public int ActorRoleId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public Movie Movie { get; set; } = null!;

    [ForeignKey(nameof(ActorId))]
    public Actor Actor { get; set; } = null!;

    [ForeignKey(nameof(ActorRoleId))]
    public ActorRole ActorRole { get; set; } = null!;
}