using Microsoft.EntityFrameworkCore;
using movies_backend.Data;
using movies_backend.DTOs;
using movies_backend.Model;

namespace movies_backend.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationContext _context;

    public MovieRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesRatingExist(int ratingId)
    {
        return await _context.Ratings.AnyAsync(r => r.Id == ratingId);
    }

    public async Task<bool> DoesGenreExist(int genreId)
    {
        return await _context.Genres.AnyAsync(g => g.Id == genreId);
    }

    public async Task<int> AddNewGenre(string newGenreName)
    {
        var genre = new Genre { Name = newGenreName };
        await _context.AddAsync(genre);
        await _context.SaveChangesAsync();

        return genre.Id;
    }

    public async Task<ICollection<GetGenreDto>> GetAllGenres()
    {
        var genres = await _context.Genres.ToListAsync();

        return genres.Select(genre => new GetGenreDto
        {
            Name = genre.Name
        }).ToList();
    }

    public async Task<bool> DoesMovieExist(int movieId)
    {
        return await _context.Movies.AnyAsync(m => m.Id == movieId);
    }

    public async Task<GetMovieDto?> GetMovieById(int id)
    {
        var movie = await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.MovieRatings)
            .ThenInclude(mr => mr.Rating)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.Actor)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.ActorRole)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.Actor.Gender)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return null;
        }

        return new GetMovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Director = movie.Director,
            ReleaseDate = movie.ReleaseDate,
            Duration = movie.Duration,
            TmdbId = movie.TmdbId,
            PosterUrl = movie.PosterUrl,
            AverageRating = movie.GetAverageRating(),
            Genres = movie.Genres.Select(g => new GetGenreDto { Name = g.Name }).ToList(),
            Actors = movie.Actors.Select(ma => new GetActorWithRoleDto
            {
                Name = ma.Actor.Name,
                GenderName = ma.Actor.Gender.Name,
                BirthDate = ma.Actor.BirthDate,
                DeathDate = ma.Actor.DeathDate,
                Biography = ma.Actor.Biography,
                RoleName = ma.ActorRole.Name
            }).ToList()
        };
    }


    public async Task<GetMovieDto?> GetMovieByTitle(string title)
    {
        var movie = await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.MovieRatings)
            .ThenInclude(mr => mr.Rating)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.Actor)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.ActorRole)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.Actor.Gender)
            .FirstOrDefaultAsync(m => m.Title == title);

        if (movie == null)
        {
            return null;
        }

        return new GetMovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Director = movie.Director,
            ReleaseDate = movie.ReleaseDate,
            Duration = movie.Duration,
            TmdbId = movie.TmdbId,
            PosterUrl = movie.PosterUrl,
            AverageRating = movie.GetAverageRating(),
            Genres = movie.Genres.Select(g => new GetGenreDto { Name = g.Name }).ToList(),
            Actors = movie.Actors.Select(ma => new GetActorWithRoleDto
            {
                Name = ma.Actor.Name,
                GenderName = ma.Actor.Gender.Name,
                BirthDate = ma.Actor.BirthDate,
                DeathDate = ma.Actor.DeathDate,
                Biography = ma.Actor.Biography,
                RoleName = ma.ActorRole.Name
            }).ToList()
        };
    }

    public async Task<GetMovieDto?> GetMovieByTmdbId(string tmdbId)
    {
        var movie = await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.MovieRatings)
            .ThenInclude(mr => mr.Rating)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.Actor)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.ActorRole)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.Actor.Gender)
            .FirstOrDefaultAsync(m => m.TmdbId == tmdbId);

        if (movie == null)
        {
            return null;
        }

        return new GetMovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Director = movie.Director,
            ReleaseDate = movie.ReleaseDate,
            Duration = movie.Duration,
            TmdbId = movie.TmdbId,
            PosterUrl = movie.PosterUrl,
            AverageRating = movie.GetAverageRating(),
            Genres = movie.Genres.Select(g => new GetGenreDto { Name = g.Name }).ToList(),
            Actors = movie.Actors.Select(ma => new GetActorWithRoleDto
            {
                Name = ma.Actor.Name,
                GenderName = ma.Actor.Gender.Name,
                BirthDate = ma.Actor.BirthDate,
                DeathDate = ma.Actor.DeathDate,
                Biography = ma.Actor.Biography,
                RoleName = ma.ActorRole.Name
            }).ToList()
        };
    }

    public async Task<Double> GetAverageMovieRating(int movieId)
    {
        return await _context.Movies
            .Include(m => m.MovieRatings)
            .ThenInclude(mr => mr.Rating)
            .FirstOrDefaultAsync(m => m.Id == movieId)
            .ContinueWith(m => m.Result?.GetAverageRating() ?? 0);
    }

    public async Task<ICollection<GetMovieDto>> GetAllMovies()
    {
        var movies = await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.MovieRatings)
            .ThenInclude(mr => mr.Rating)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.Actor)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.ActorRole)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.Actor.Gender)
            .ToListAsync();


        return movies.Select(movie => new GetMovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Director = movie.Director,
            ReleaseDate = movie.ReleaseDate,
            Duration = movie.Duration,
            TmdbId = movie.TmdbId,
            PosterUrl = movie.PosterUrl,
            AverageRating = movie.GetAverageRating(),
            Genres = movie.Genres.Select(g => new GetGenreDto { Name = g.Name }).ToList(),
            Actors = movie.Actors.Select(ma => new GetActorWithRoleDto
            {
                Name = ma.Actor.Name,
                GenderName = ma.Actor.Gender.Name,
                BirthDate = ma.Actor.BirthDate,
                DeathDate = ma.Actor.DeathDate,
                Biography = ma.Actor.Biography,
                RoleName = ma.ActorRole.Name
            }).ToList()
        }).ToList();
    }

    /*public async Task<int> AddNewMovie(AddNewMovieDto addNewMovieDto)
    {
        // Tworzenie filmu
        var movie = new Movie
        {
            Title = addNewMovieDto.Title,
            Director = addNewMovieDto.Director,
            ReleaseDate = new DateOnly(addNewMovieDto.ReleaseDate.Year, addNewMovieDto.ReleaseDate.Month, addNewMovieDto.ReleaseDate.Day),
            Duration = addNewMovieDto.Duration,
            TmdbId = addNewMovieDto.TmdbId
        };

        // Przypisanie gatunków
        var genres = await _context.Genres
            .Where(g => addNewMovieDto.Genres.Contains(g.Name))
            .ToListAsync();

        foreach (var genreName in addNewMovieDto.Genres.Except(genres.Select(g => g.Name)))
        {
            var newGenre = new Genre { Name = genreName };
            genres.Add(newGenre);
            await _context.Genres.AddAsync(newGenre);
        }
        movie.Genres = genres;

        // Przypisanie aktorów i ról
        foreach (var actorDto in addNewMovieDto.Actors)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Name == actorDto.Name);

            if (actor == null)
            {
                actor = new Actor
                {
                    Name = actorDto.Name,
                    GenderId = actorDto.GenderId,
                    BirthDate = new DateOnly(actorDto.BirthDate.Year, actorDto.BirthDate.Month, actorDto.BirthDate.Day),
                    DeathDate = new DateOnly(actorDto.DeathDate.Year, actorDto.DeathDate.Month, actorDto.DeathDate.Day),
                    Biography = actorDto.Biography
                };
                await _context.Actors.AddAsync(actor);
            }

            var role = await _context.ActorRoles.FirstOrDefaultAsync(r => r.Name == actorDto.RoleName);
            if (role == null)
            {
                role = new ActorRole { Name = actorDto.RoleName };
                await _context.ActorRoles.AddAsync(role);
            }

            movie.Actors.Add(new MovieActor
            {
                Actor = actor,
                ActorRole = role
            });
        }

        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();

        return movie.Id;
    }*/

    public async Task<int> AddNewMovie(AddNewMovieDto addNewMovieDto)
    {
        // Tworzenie filmu
        var movie = new Movie
        {
            Title = addNewMovieDto.Title,
            Director = addNewMovieDto.Director,
            ReleaseDate = new DateOnly(addNewMovieDto.ReleaseDate.Year, addNewMovieDto.ReleaseDate.Month, addNewMovieDto.ReleaseDate.Day),
            Duration = addNewMovieDto.Duration,
            TmdbId = addNewMovieDto.TmdbId,
            PosterUrl = addNewMovieDto.PosterUrl
        };

        // Sprawdzenie istnienia gatunków
        /*ar genres = await _context.Genres
            .Where(g => addNewMovieDto.Genres.Contains(g.Name))
            .ToListAsync();

        if (genres.Count != addNewMovieDto.Genres.Count)
        {
            throw new Exception("One or more genres do not exist.");
        }
        movie.Genres = genres;*/

        var genres = await _context.Genres
            .Where(g => addNewMovieDto.Genres.Contains(g.Name))
            .ToListAsync();

        var missingGenres = addNewMovieDto.Genres
            .Except(genres.Select(g => g.Name), StringComparer.Ordinal);

        if (missingGenres.Any())
        {
            throw new Exception($"One or more genres do not exist: {string.Join(", ", missingGenres)}");
        }

        movie.Genres = genres;

        // Przypisanie aktorów i ról
        foreach (var actorDto in addNewMovieDto.Actors)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Name == actorDto.Name);

            if (actor == null)
            {
                actor = new Actor
                {
                    Name = actorDto.Name,
                    GenderId = actorDto.GenderId,
                    BirthDate = new DateOnly(actorDto.BirthDate.Year, actorDto.BirthDate.Month, actorDto.BirthDate.Day),
                    DeathDate = new DateOnly(actorDto.DeathDate.Year, actorDto.DeathDate.Month, actorDto.DeathDate.Day),
                    Biography = actorDto.Biography
                };
                await _context.Actors.AddAsync(actor);
            }

            var role = await _context.ActorRoles.FirstOrDefaultAsync(r => r.Name == actorDto.RoleName);
            if (role == null)
            {
                throw new Exception($"Role '{actorDto.RoleName}' does not exist.");
            }

            movie.Actors.Add(new MovieActor
            {
                Actor = actor,
                ActorRole = role
            });
        }

        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();

        return movie.Id;
    }

    public async Task<ICollection<GetRatingDto>> GetMovieRatings(int movieId)
    {
        var movie = await _context.Movies
            .Include(m => m.MovieRatings)
            .ThenInclude(mr => mr.Rating)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie == null)
        {
            return new List<GetRatingDto>();
        }

        return movie.MovieRatings.Select(mr => new GetRatingDto
        {
            StarsNumber = mr.Rating.StarsNumber
        }).ToList();
    }

    public async Task<bool> AddMovieRating(int movieId, int ratingId)
    {
        var movie = await _context.Movies
            .Include(m => m.MovieRatings)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie == null)
        {
            return false;
        }

        var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.Id == ratingId);

        if (rating == null)
        {
            return false;
        }

        movie.MovieRatings.Add(new MovieRating
        {
            MovieId = movieId,
            RatingId = ratingId
        });

        await _context.SaveChangesAsync();

        return true;
    }
}