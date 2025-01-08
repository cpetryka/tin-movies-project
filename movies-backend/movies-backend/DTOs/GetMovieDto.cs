using movies_backend.Model;

namespace movies_backend.DTOs;

public class GetMovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Director { get; set; } = null!;
    public DateOnly ReleaseDate { get; set; }
    public int Duration { get; set; }
    public string TmdbId { get; set; } = null!;
    public string PosterUrl { get; set; } = null!;
    public double AverageRating { get; set; }
    public IList<GetGenreDto> Genres { get; set; } = new List<GetGenreDto>();
    public IList<GetActorWithRoleDto> Actors { get; set; } = new List<GetActorWithRoleDto>();
}