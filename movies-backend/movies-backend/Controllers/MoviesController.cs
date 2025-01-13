using Microsoft.AspNetCore.Authorization;
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

    /*************************************************************************************************
     * GENRES MANAGEMENT
     *************************************************************************************************/

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

    [HttpPut("update-genre-by-id")]
    public async Task<IActionResult> UpdateGenreById([FromQuery] int genreId, [FromBody] string newGenreName)
    {
        var updatedGenre = await _movieRepository.UpdateGenreById(genreId, newGenreName);

        if (updatedGenre == null)
        {
            return NotFound(new { message = "Genre not found." });
        }

        return Ok(new
        {
            message = "Genre updated successfully.",
            updatedGenre
        });
    }

    [HttpDelete("delete-genre-by-id")]
    public async Task<IActionResult> DeleteGenreById([FromQuery] int genreId)
    {
        var deletedGenre = await _movieRepository.DeleteGenreById(genreId);

        if (deletedGenre == null)
        {
            return NotFound(new { message = "Genre not found." });
        }

        return Ok(new
        {
            message = "Genre deleted successfully.",
            deletedGenre
        });
    }

    /*************************************************************************************************
     * MOVIES MANAGEMENT
     *************************************************************************************************/

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

    [HttpGet("get-all-movies")]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _movieRepository.GetAllMovies();

        return Ok(movies);
    }

    [HttpGet("get-all-movies-segmented")]
    public async Task<IActionResult> GetAllMoviesSegmented([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Page and page size must be greater than 0.");
        }

        var movies = await _movieRepository.GetAllMoviesSegmented(page, pageSize);
        return Ok(movies);
    }

    [HttpGet("get-all-movies-segmented-with-count")]
    public async Task<IActionResult> GetAllMoviesSegmentedWithCount([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Page and page size must be greater than 0.");
        }

        var (movies, totalCount) = await _movieRepository.GetAllMoviesSegmentedWithCount(page, pageSize);

        return Ok(new
        {
            Movies = movies,
            TotalCount = totalCount
        });
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Query cannot be empty.");
        }

        var results = await _movieRepository.SearchMoviesAsync(query);

        return Ok(results);
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

    [Authorize(Roles = "Admin")]
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

    [HttpPut("update-movie-by-id")]
    public async Task<IActionResult> UpdateMovie([FromQuery] int movieId, [FromBody] AddNewMovieDto addNewMovieDto)
    {
        try
        {
            var updatedMovie = await _movieRepository.UpdateMovieById(movieId, addNewMovieDto);
            if (updatedMovie == null)
            {
                return NotFound($"Movie with ID {movieId} not found.");
            }
            return Ok(updatedMovie);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete-movie-by-id")]
    public async Task<IActionResult> DeleteMovie([FromQuery] int movieId)
    {
        try
        {
            var result = await _movieRepository.DeleteMovie(movieId);
            if (!result)
            {
                return NotFound($"Movie with ID {movieId} not found.");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /*************************************************************************************************
     * RATINGS MANAGEMENT
     *************************************************************************************************/

    [HttpGet("get-movie-ratings")]
    public async Task<IActionResult> GetMovieRatings([FromQuery] int movieId)
    {
        var ratings = await _movieRepository.GetMovieRatings(movieId);

        return Ok(ratings);
    }

    [HttpGet("get-all-movie-ratings-added-by")]
    public async Task<IActionResult> GetAllMovieRatingsAddedBy([FromQuery] int userId)
    {
        var ratings = await _movieRepository.GetAllMovieRatingsAddedBy(userId);

        return Ok(ratings);
    }

    [Authorize(Roles = "User")]
    [HttpPost("add-movie-rating")]
    public async Task<IActionResult> AddMovieRating([FromQuery] int movieId, [FromQuery] int ratingId, [FromQuery] int userId)
    {
        var success = await _movieRepository.AddMovieRating(movieId, ratingId, userId);

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

    [HttpPut("update-movie-rating-added-by")]
    public async Task<IActionResult> UpdateMovieRatingAddedBy([FromQuery] int movieId, [FromQuery] int userId, [FromQuery] int newRatingId)
    {
        var updatedRating = await _movieRepository.UpdateMovieRatingAddedBy(movieId, userId, newRatingId);

        if (updatedRating == null)
        {
            return NotFound(new
            {
                message = "Rating not found"
            });
        }

        return Ok(new
        {
            message = "Rating updated successfully",
            updatedRating
        });
    }

    [HttpDelete("delete-movie-rating-added-by")]
    public async Task<IActionResult> DeleteMovieRatingAddedBy([FromQuery] int movieId, [FromQuery] int userId)
    {
        var success = await _movieRepository.DeleteMovieRatingAddedBy(movieId, userId);

        if (!success)
        {
            return BadRequest(new
            {
                message = "Rating not found"
            });
        }

        return NoContent();
    }
}