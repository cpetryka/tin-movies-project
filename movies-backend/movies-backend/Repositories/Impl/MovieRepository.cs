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

    /*************************************************************************************************
     * GENRES MANAGEMENT
     *************************************************************************************************/

    public async Task<bool> DoesGenreExist(int genreId)
    {
        return await _context.Genres.AnyAsync(g => g.Id == genreId);
    }

    public async Task<ICollection<GetGenreDto>> GetAllGenres()
    {
        var genres = await _context.Genres.ToListAsync();

        return genres.Select(genre => new GetGenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        }).ToList();
    }

    public async Task<int> AddNewGenre(string newGenreName)
    {
        var genre = new Genre { Name = newGenreName };
        await _context.AddAsync(genre);
        await _context.SaveChangesAsync();

        return genre.Id;
    }

    public async Task<GetGenreDto?> UpdateGenreById(int genreId, string newGenreName)
    {
        var genre = await _context.Genres.FindAsync(genreId);

        if (genre == null)
        {
            return null;
        }

        genre.Name = newGenreName;

        await _context.SaveChangesAsync();

        return new GetGenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    public async Task<GetGenreDto?> DeleteGenreById(int genreId)
    {
        var genre = await _context.Genres.FindAsync(genreId);

        if (genre == null)
        {
            return null;
        }

        _context.Genres.Remove(genre);

        await _context.SaveChangesAsync();

        return new GetGenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    /*************************************************************************************************
     * MOVIES MANAGEMENT
     *************************************************************************************************/

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

    public async Task<ICollection<GetMovieDto>> GetAllMoviesSegmented(int page, int pageSize)
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
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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

    public async Task<(ICollection<GetMovieDto> Movies, int TotalCount)> GetAllMoviesSegmentedWithCount(int page, int pageSize)
    {
        var totalCount = await _context.Movies.CountAsync();

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
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var movieDtos = movies.Select(movie => new GetMovieDto
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

        return (movieDtos, totalCount);
    }
    
    public async Task<ICollection<GetMovieDto>> SearchMoviesAsync(string query)
    {
        query = query.ToLower();

        var movies = await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.Actor)
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.ActorRole)
            .Include(ma => ma.Actors)
            .ThenInclude(ma => ma.Actor.Gender)
            .Include(ma => ma.MovieRatings)
            .ThenInclude(ma => ma.Rating)
            .ToListAsync();

        var filteredMovies = movies
            .Where(movie => string.Join(" ",
                movie.Title,
                movie.Director,
                movie.ReleaseDate.ToString("yyyy-MM-dd"),
                movie.Duration,
                movie.TmdbId,
                string.Join(" ", movie.Genres.Select(g => g.Name)),
                string.Join(" ",
                    movie.Actors.Select(a =>
                        $"{a.Actor.Name} {a.ActorRole.Name} {a.Actor.Gender.Name} {a.Actor.Biography}"))
            ).ToLower().Contains(query))
            .Select(movie => new GetMovieDto
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
    
        return filteredMovies;
    }

    
    public async Task<Double> GetAverageMovieRating(int movieId)
    {
        return await _context.Movies
            .Include(m => m.MovieRatings)
            .ThenInclude(mr => mr.Rating)
            .FirstOrDefaultAsync(m => m.Id == movieId)
            .ContinueWith(m => m.Result?.GetAverageRating() ?? 0);
    }

    public async Task<int> AddNewMovie(AddNewMovieDto addNewMovieDto)
    {
        var movie = new Movie
        {
            Title = addNewMovieDto.Title,
            Director = addNewMovieDto.Director,
            ReleaseDate = new DateOnly(addNewMovieDto.ReleaseDate.Year, addNewMovieDto.ReleaseDate.Month, addNewMovieDto.ReleaseDate.Day),
            Duration = addNewMovieDto.Duration,
            TmdbId = addNewMovieDto.TmdbId,
            PosterUrl = addNewMovieDto.PosterUrl
        };

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

    public async Task<GetMovieDto?> UpdateMovieById(int movieId, AddNewMovieDto addNewMovieDto)
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
            .FirstOrDefaultAsync(m => m.Id == movieId);

    if (movie == null)
    {
        throw new Exception($"Movie with ID {movieId} not found.");
    }

    movie.Title = addNewMovieDto.Title;
    movie.Director = addNewMovieDto.Director;
    movie.ReleaseDate = new DateOnly(addNewMovieDto.ReleaseDate.Year, addNewMovieDto.ReleaseDate.Month, addNewMovieDto.ReleaseDate.Day);
    movie.Duration = addNewMovieDto.Duration;
    movie.TmdbId = addNewMovieDto.TmdbId;
    movie.PosterUrl = addNewMovieDto.PosterUrl;

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

    var actorRoles = await _context.ActorRoles.ToListAsync();
    movie.Actors.Clear();

    foreach (var actorDto in addNewMovieDto.Actors)
    {
        var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Name == actorDto.Name);
        if (actor == null)
        {
            throw new Exception($"Actor '{actorDto.Name}' does not exist.");
        }

        var role = actorRoles.FirstOrDefault(r => r.Name == actorDto.RoleName);
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

    await _context.SaveChangesAsync();

    return new GetMovieDto
    {
        Id = movie.Id,
        Title = movie.Title,
        Director = movie.Director,
        ReleaseDate = movie.ReleaseDate,
        Duration = movie.Duration,
        TmdbId = movie.TmdbId,
        PosterUrl = movie.PosterUrl,
        Genres = movie.Genres.Select(g => new GetGenreDto
        {
            Id = g.Id,
            Name = g.Name
        }).ToList(),
        Actors = movie.Actors.Select(a => new GetActorWithRoleDto
        {
            Name = a.Actor.Name,
            GenderName = a.Actor.Gender.Name,
            BirthDate = a.Actor.BirthDate,
            DeathDate = a.Actor.DeathDate,
            Biography = a.Actor.Biography,
            RoleName = a.ActorRole.Name
        }).ToList()
    };
}


    public async Task<bool> DeleteMovie(int movieId)
    {
        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie == null)
        {
            throw new Exception($"Movie with ID {movieId} not found.");
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return true;
    }
    
    /*************************************************************************************************
     * RATINGS MANAGEMENT
     *************************************************************************************************/

    public async Task<bool> DoesRatingExist(int ratingId)
    {
        return await _context.Ratings.AnyAsync(r => r.Id == ratingId);
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

    public async Task<ICollection<GetRatingWithMovieDto>> GetAllMovieRatingsAddedBy(int userId)
    {
        return await _context.MovieRatings
            .Include(mr => mr.Movie)
            .Include(mr => mr.Rating)
            .Where(mr => mr.UserId == userId)
            .Select(mr => new GetRatingWithMovieDto
            {
                MovieRatingId = mr.Id,
                MovieId = mr.MovieId,
                MovieTitle = mr.Movie.Title,
                MovieDirector = mr.Movie.Director,
                MovieReleaseDate = mr.Movie.ReleaseDate,
                StarsNumber = mr.Rating.StarsNumber
            })
            .ToListAsync();
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

    public async Task<bool> AddMovieRating(int movieId, int ratingId, int userId)
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
            RatingId = ratingId,
            UserId = userId
        });

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<GetRatingWithMovieDto?> UpdateMovieRatingAddedBy(int movieId, int userId, int newRatingId)
    {
        var movieRating = await _context.MovieRatings
            .Include(mr => mr.Movie)
            .Include(mr => mr.Rating)
            .FirstOrDefaultAsync(mr => mr.MovieId == movieId && mr.UserId == userId);

        if (movieRating == null)
        {
            return null;
        }

        movieRating.RatingId = newRatingId;
        await _context.SaveChangesAsync();

        return new GetRatingWithMovieDto
        {
            MovieRatingId = movieRating.Id,
            MovieId = movieRating.MovieId,
            MovieTitle = movieRating.Movie.Title,
            MovieDirector = movieRating.Movie.Director,
            MovieReleaseDate = movieRating.Movie.ReleaseDate,
            StarsNumber = movieRating.Rating.StarsNumber
        };
    }

    public async Task<bool> DeleteMovieRatingAddedBy(int movieId, int userId)
    {
        var movieRating = await _context.MovieRatings
            .FirstOrDefaultAsync(m => m.MovieId == movieId && m.UserId == userId);

        if (movieRating == null)
        {
            throw new Exception($"Rating for movie with ID {movieId} added by user with ID {userId} not found.");
        }

        _context.MovieRatings.Remove(movieRating);
        await _context.SaveChangesAsync();

        return true;
    }
}