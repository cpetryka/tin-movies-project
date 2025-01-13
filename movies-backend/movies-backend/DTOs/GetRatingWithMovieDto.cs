namespace movies_backend.DTOs;

public class GetRatingWithMovieDto
{
    public int MovieRatingId { get; set; }
    public int StarsNumber { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; } = null!;
    public string MovieDirector { get; set; } = null!;
    public DateOnly MovieReleaseDate { get; set; }
}