using Microsoft.EntityFrameworkCore;
using movies_backend.Model;
using movies_backend.Model.User;

namespace movies_backend.Data;

public class ApplicationContext : DbContext
{
    protected ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieActor> MovieActors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>().HasData(new List<UserRole>()
        {
            new() { Id = 1, Name = "Admin"},
            new() { Id = 2, Name = "User"},
            new() { Id = 3, Name = "Guest"}
        });

        modelBuilder.Entity<User>().HasData(new List<User>()
        {
            new() { Id = 1, Name = "Czarek", Email = "cezaryp10@gmail.com", Password = "1234", UserRoleId = 1 },
            new() { Id = 2, Name = "John", Email = "john@gmail.com", Password = "1234", UserRoleId = 2 }
        });

        modelBuilder.Entity<Genre>().HasData(new List<Genre>()
        {
            new() { Id = 1, Name = "Action"},
            new() { Id = 2, Name = "Comedy"},
            new() { Id = 3, Name = "Drama"},
            new() { Id = 4, Name = "Horror"},
            new() { Id = 5, Name = "Sci-Fi"},
            new() { Id = 6, Name = "Thriller"}
        });

        modelBuilder.Entity<Rating>().HasData(new List<Rating>()
        {
            new() { Id = 1, StarsNumber = 1},
            new() { Id = 2, StarsNumber = 2},
            new() { Id = 3, StarsNumber = 3},
            new() { Id = 4, StarsNumber = 4},
            new() { Id = 5, StarsNumber = 5}
        });

        modelBuilder.Entity<Gender>().HasData(new List<Gender>()
        {
            new() { Id = 1, Name = "Male" },
            new() { Id = 2, Name = "Female" }
        });

        modelBuilder.Entity<Actor>().HasData(new List<Actor>()
        {
            new() { Id = 1, Name = "Tom Holland", GenderId = 1, BirthDate = new DateOnly(1996, 6, 1), Biography = "Thomas \"Tom\" Stanley Holland is an English actor and dancer.\n\nHe is best known for playing Peter Parker / Spider-Man in the Marvel Cinematic Universe and has appeared as the character in six films: Captain America: Civil War (2016), Spider-Man: Homecoming (2017), Avengers: Infinity War (2018), Avengers: Endgame (2019), Spider-Man: Far From Home (2019), and Spider-Man: No Way Home (2021).\n\nHe is also known for playing the title role in Billy Elliot the Musical at the Victoria Palace Theatre, London, as well as for starring in the 2012 film The Impossible." },
            new() { Id = 2, Name = "Zendaya", GenderId = 2, BirthDate = new DateOnly(1996, 9, 1), Biography = "Zendaya Maree Stoermer Coleman (born September 1, 1996) is an American actress and singer. She began her career as a child model and backup dancer. She rose to mainstream prominence for her role as Rocky Blue on the Disney Channel sitcom Shake It Up (2010–2013).\n\nIn 2013, Zendaya was a contestant on the 16th season of the dance competition series Dancing with the Stars. She produced and starred as the titular spy, K.C. Cooper, in the sitcom K.C. Undercover (2015–2018). Her film roles include the musical drama The Greatest Showman (2017), the superhero film Spider-Man: Homecoming (2017) and its sequels, the computer-animated musical comedy Smallfoot (2018), the romantic drama Malcolm & Marie (2021), the live-action/animation hybrid sports comedy Space Jam: A New Legacy (2021), and the science fiction epic Dune (2021)." },
            new() { Id = 3, Name = "Benedict Cumberbatch", GenderId = 1, BirthDate = new DateOnly(1976, 7, 19), Biography = "Benedict Timothy Carlton Cumberbatch CBE (born 19 July 1976) is an English actor. Known for his work in film, television, and theatre, he has received various accolades throughout his career, including a Laurence Olivier Award, a Golden Globe Award, and a British Academy Television Award.\n\nCumberbatch's breakthrough role came in 2004 when he portrayed Stephen Hawking in the television film Hawking. In 2010, he became a household name for playing Sherlock Holmes in the British television series Sherlock, which earned him the British Academy Television Award for Best Actor. In 2011, Cumberbatch made his feature film debut in the historical drama War Horse. He rose to international prominence for his performance as Julian Assange in the biographical film The Fifth Estate (2013) and as Khan Noonien Singh in the science fiction film Star Trek Into Darkness (2013)." },
            new() { Id = 4, Name = "Josephine Langford", GenderId = 2, BirthDate = new DateOnly(1997, 8, 18), Biography = "Josephine Langford (born 18 August 1997) is an Australian actress. She is known for her role as Tessa Young in the After film series, and as Nat in the film Wish Upon (2017)." },
            new() { Id = 5, Name = "Hero Fiennes Tiffin", GenderId = 1, BirthDate = new DateOnly(1997, 11, 6), Biography = "Hero Fiennes Tiffin (born 6 November 1997) is an English actor and model born in London, England. He is best known for his role as the 11-year-old Tom Riddle, the young version of Lord Voldemort (played in the films by his uncle, Ralph Fiennes) in Harry Potter and the Half Blood Prince (2009). He portrayed Hardin Scott in the movie After (2019)." },
            new() { Id = 6, Name = "Anthony Ramos", GenderId = 1, BirthDate = new DateOnly(1991, 11, 1), Biography = "Anthony Ramos (born 1 November 1991) is an American actor and singer. He is known for his roles in the musicals In the Heights and Hamilton, as well as starring in films like A Star is Born (2018) and In the Heights (2021)." },
            new() { Id = 7, Name = "Dominique Fishback", GenderId = 2, BirthDate = new DateOnly(1991, 3, 22), Biography = "Dominique Fishback (born 22 March 1991) is an American actress and playwright. She is best known for her roles in HBO's The Deuce, as well as her role as Deborah Johnson in the film Judas and the Black Messiah (2021) and in the movie Transformers: Rise of the Beasts (2023)." },
            new() { Id = 8, Name = "Luna Lauren Velez", GenderId = 2, BirthDate = new DateOnly(1964, 11, 2), Biography = "Luna Lauren Velez (born 2 November 1964) is an American actress. She is well known for her roles as María LaGuerta on the television series Dexter and as Elena in the film Spider-Man: Into the Spider-Verse (2018). She also starred in New York Undercover and Oz." },
            new() { Id = 9, Name = "Mark Wahlberg", GenderId = 1, BirthDate = new DateOnly(1971, 6, 5), Biography = "Mark Wahlberg (born 5 June 1971) is an American actor, producer, and former rapper. Known for his roles in films like The Departed (2006), Transformers: Age of Extinction (2014), and Daddy's Home (2015), he first gained fame as a member of the music group Marky Mark and the Funky Bunch in the 1990s." },
            new() { Id = 10, Name = "Sophia Ali", GenderId = 2, BirthDate = new DateOnly(1995, 11, 7), Biography = "Sophia Ali (born 7 November 1995) is an American actress. She is best known for her roles as Dr. Dahlia Qadri on Grey's Anatomy and as Chloe Frazer in the film Uncharted (2022)." }
        });

        modelBuilder.Entity<ActorRole>().HasData(new List<ActorRole>()
        {
            new() { Id = 1, Name = "Lead" },
            new() { Id = 2, Name = "Supporting" }
        });

        modelBuilder.Entity<Movie>().HasData(new List<Movie>()
        {
            new() { Id = 1, Title = "SPIDER MAN NO WAY HOME", Director = "JON WATTS", ReleaseDate = new DateOnly(2021, 12, 17), Duration = 148, TmdbId = "634649" },
            new() { Id = 2, Title = "AFTER", Director = "JENNY GAGE", ReleaseDate = new DateOnly(2019, 4, 12), Duration = 105, TmdbId = "537915" },
            new() { Id = 3, Title = "TRANSFORMERS RISE OF THE BEASTS", Director = "STEVEN CAPLE JR", ReleaseDate = new DateOnly(2023, 6, 6), Duration = 128, TmdbId = "667538" },
            new() { Id = 4, Title = "UNCHARTED", Director = "RUBEN FLEISCHER", ReleaseDate = new DateOnly(2022, 2, 10), Duration = 116, TmdbId = "335787" },
            new() { Id = 5, Title = "THE LUCKY ONE", Director = "SCOTT HICKS", ReleaseDate = new DateOnly(2012, 4, 20), Duration = 101, TmdbId = "77877" }
        });

        modelBuilder.Entity<MovieActor>().HasData(new List<MovieActor>()
        {
            new() { MovieId = 1, ActorId = 1, ActorRoleId = 1 },
            new() { MovieId = 1, ActorId = 2, ActorRoleId = 2 },
            new() { MovieId = 1, ActorId = 3, ActorRoleId = 2 },
            new() { MovieId = 2, ActorId = 4, ActorRoleId = 1 },
            new() { MovieId = 2, ActorId = 5, ActorRoleId = 2 },
            new() { MovieId = 3, ActorId = 6, ActorRoleId = 1 },
            new() { MovieId = 3, ActorId = 7, ActorRoleId = 2 },
            new() { MovieId = 3, ActorId = 8, ActorRoleId = 2 },
            new() { MovieId = 4, ActorId = 1, ActorRoleId = 1 },
            new() { MovieId = 4, ActorId = 9, ActorRoleId = 2 },
            new() { MovieId = 4, ActorId = 10, ActorRoleId = 2 },
            new() { MovieId = 5, ActorId = 9, ActorRoleId = 1 },
            new() { MovieId = 5, ActorId = 10, ActorRoleId = 2 }
        });
    }
}