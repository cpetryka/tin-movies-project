using movies_backend.DTOs;

namespace movies_backend.Repositories;

public interface IMovieRepository
{
    Task<bool> DoesRatingExist(int ratingId);

    Task<bool> DoesGenreExist(int genreId);
    Task<int> AddNewGenre(string newGenreName);
    Task<ICollection<GetGenreDto>> GetAllGenres();

    Task<bool> DoesMovieExist(int movieId);
    Task<GetMovieDto?> GetMovieById(int id);
    Task<GetMovieDto?> GetMovieByTitle(string title);
    Task<Double> GetAverageMovieRating(int movieId);
    Task<ICollection<GetMovieDto>> GetAllMovies();
    Task<int> AddNewMovie(AddNewMovieDto addNewMovieDto);
    Task<bool> AddMovieRating(int movieId, int ratingId);
}