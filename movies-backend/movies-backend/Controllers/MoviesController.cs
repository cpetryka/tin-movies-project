using Microsoft.AspNetCore.Mvc;
using movies_backend.DTOs;
using movies_backend.Repositories;

namespace movies_backend.Controllers;

[Route("api/movies")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MoviesController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet("get-all-movies")]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _movieRepository.GetAllMovies();

        return Ok(movies);
    }

    [HttpGet("get-movie-by-id")]
    public async Task<IActionResult> GetMovieById([FromQuery] int id)
    {
        var movie = await _movieRepository.GetMovieById(id);

        if (movie == null)
        {
            return NotFound(new
            {
                message = "Movie not found"
            });
        }

        return Ok(movie);
    }

    [HttpGet("get-movie-by-title")]
    public async Task<IActionResult> GetMovieByTitle([FromQuery] string title)
    {
        var movie = await _movieRepository.GetMovieByTitle(title);

        if (movie == null)
        {
            return NotFound(new
            {
                message = "Movie not found"
            });
        }

        return Ok(movie);
    }

    [HttpGet("get-movie-by-tmdb-id")]
    public async Task<IActionResult> GetMovieByTmdbId([FromQuery] string tmdbId)
    {
        var movie = await _movieRepository.GetMovieByTmdbId(tmdbId);

        if (movie == null)
        {
            return NotFound(new
            {
                message = "Movie not found"
            });
        }

        return Ok(movie);
    }

    [HttpGet("get-average-movie-rating")]
    public async Task<IActionResult> GetAverageMovieRating([FromQuery] int movieId)
    {
        var averageRating = await _movieRepository.GetAverageMovieRating(movieId);

        return Ok(new
        {
            averageRating
        });
    }

    [HttpPost("add-new-movie")]
    public async Task<IActionResult> AddNewMovie([FromBody] AddNewMovieDto addNewMovieDto)
    {
        var movieId = await _movieRepository.AddNewMovie(addNewMovieDto);

        return Created("", new
        {
            message = "Movie added successfully",
            addedMovieId = movieId
        });
    }

    [HttpGet("get-movie-ratings")]
    public async Task<IActionResult> GetMovieRatings([FromQuery] int movieId)
    {
        var ratings = await _movieRepository.GetMovieRatings(movieId);

        return Ok(ratings);
    }

    [HttpPost("add-movie-rating")]
    public async Task<IActionResult> AddMovieRating([FromQuery] int movieId, [FromQuery] int ratingId)
    {
        var success = await _movieRepository.AddMovieRating(movieId, ratingId);

        if (!success)
        {
            return BadRequest(new
            {
                message = "Rating or movie not found"
            });
        }

        return Created("", new
        {
            message = "Rating added successfully"
        });
    }

    [HttpGet("get-all-genres")]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _movieRepository.GetAllGenres();

        return Ok(genres);
    }

    [HttpPost("add-new-genre")]
    public async Task<IActionResult> AddNewGenre([FromBody] string genreName)
    {
        var id = await _movieRepository.AddNewGenre(genreName);

        return Created("", new
        {
            message = "Genre added successfully",
            addedGenreId = id
        });
    }
}