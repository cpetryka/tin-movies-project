using System.Collections;
using movies_backend.DTOs;

namespace movies_backend.Repositories;

public interface IMovieRepository
{


    Task<bool> DoesGenreExist(int genreId);
    Task<ICollection<GetGenreDto>> GetAllGenres();
    Task<int> AddNewGenre(string newGenreName);
    Task<GetGenreDto?> UpdateGenreById(int genreId, string newGenreName);
    Task<GetGenreDto?> DeleteGenreById(int genreId);

    Task<bool> DoesMovieExist(int movieId);
    Task<GetMovieDto?> GetMovieById(int id);
    Task<GetMovieDto?> GetMovieByTitle(string title);
    Task<GetMovieDto?> GetMovieByTmdbId(string tmdbId);
    Task<Double> GetAverageMovieRating(int movieId);
    Task<ICollection<GetMovieDto>> GetAllMovies();
    Task<ICollection<GetMovieDto>> GetAllMoviesSegmented(int page, int pageSize);
    Task<(ICollection<GetMovieDto> Movies, int TotalCount)> GetAllMoviesWithCount(int page, int pageSize);
    Task<ICollection<GetMovieDto>> SearchMoviesAsync(string query);
    Task<int> AddNewMovie(AddNewMovieDto addNewMovieDto);
    Task<GetMovieDto?> UpdateMovieById(int movieId, AddNewMovieDto addNewMovieDto);
    Task<bool> DeleteMovie(int movieId);

    Task<bool> DoesRatingExist(int ratingId);
    Task<ICollection<GetRatingDto>> GetMovieRatings(int movieId);
    Task<ICollection<GetRatingWithMovieDto>> GetAllMovieRatingsAddedBy(int userId);
    Task<bool> AddMovieRating(int movieId, int ratingId);
    Task<bool> AddMovieRating(int movieId, int ratingId, int userId);
    Task<GetRatingWithMovieDto?> UpdateMovieRatingAddedBy(int movieId, int userId, int newRatingId);
    Task<bool> DeleteMovieRatingAddedBy(int movieId, int userId);
}