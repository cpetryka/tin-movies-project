using System.Collections;
using movies_backend.DTOs;

namespace movies_backend.Repositories;

public interface IMovieRepository
{
    Task<bool> DoesRatingExist(int ratingId);

    Task<bool> DoesGenreExist(int genreId);
    Task<int> AddNewGenre(string newGenreName);
    Task<GetGenreDto?> UpdateGenreById(int genreId, string newGenreName);
    Task<ICollection<GetGenreDto>> GetAllGenres();
    Task<GetGenreDto?> DeleteGenreById(int genreId);

    Task<bool> DoesMovieExist(int movieId);
    Task<GetMovieDto?> GetMovieById(int id);
    Task<GetMovieDto?> GetMovieByTitle(string title);
    Task<GetMovieDto?> GetMovieByTmdbId(string tmdbId);
    Task<Double> GetAverageMovieRating(int movieId);
    Task<ICollection<GetMovieDto>> GetAllMovies();
    Task<ICollection<GetMovieDto>> GetAllMoviesSegmented(int page, int pageSize);
    Task<(ICollection<GetMovieDto> Movies, int TotalCount)> GetAllMoviesWithCount(int page, int pageSize);
    Task<int> AddNewMovie(AddNewMovieDto addNewMovieDto);
    Task<GetMovieDto?> UpdateMovieById(int movieId, AddNewMovieDto addNewMovieDto);
    Task<bool> DeleteMovie(int movieId);
    Task<ICollection<GetMovieDto>> SearchMoviesAsync(string query);

    Task<ICollection<GetRatingDto>> GetMovieRatings(int movieId);
    Task<bool> AddMovieRating(int movieId, int ratingId);
}