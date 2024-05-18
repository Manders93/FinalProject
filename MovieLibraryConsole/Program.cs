using Microsoft.Extensions.Logging;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;

namespace MovieApp;

internal class Program
{
    private readonly string _file;

    private readonly ILogger<Program> _logger;

    public Program(ILogger<Program> logger)
    {
        _logger = logger;
        _file = $"{Environment.CurrentDirectory}/Files/movie.cs";
    }

    // Additional constructor for testing
    public Program(ILogger<Program> logger, string filePath)
    {
        _logger = logger;
        _file = filePath;
    }

    public void Invoke()
    {
        string genre;
        do
        {
            // Input genres
            var genres = new List<string>();
            do
            {
                // Ask user to enter genre
                Console.WriteLine("Enter genre (or X to quit)");
                genre = Console.ReadLine();
                // If user enters "done" or does not enter a genre do not add it to list
                if (genre != "done" && genre.Length > 0)
                {
                    genres.Add(genre);
                }
            }
            while (genre != "done");

            //_fileService.Read(); 
        }
        while (genre != "X");

        // Add userMovie
        using (var db = new MovieContext())
        {
            // build user object (not database)1
            Console.WriteLine("Enter movie title:");
            var title = Console.ReadLine();
            var user = db.Users.FirstOrDefault(u => u.Id == 943);
            var movie = db.Movies.FirstOrDefault(m => m.Title.Contains(title));

            // build user/movie relationship object (not database)
            var userMovie = new UserMovie
            {
                Rating = 2,
                RatedAt = DateTime.Now
            };

            // set up the database relationships
            userMovie.User = user;
            userMovie.Movie = movie;

            // db.Users.Add(user);
            db.UserMovies.Add(userMovie);

            // commit
            db.SaveChanges();
        }

        // Get user movie rating details
        using (var db = new MovieContext())
        {
            var users = db.Users;
            //.Include(x => x.Occupation)
            //.Include(x => x.UserMovies)
            //.ThenInclude(x => x.Movie);

            var limitedUsers = users.Take(10);

            Console.WriteLine("The users are :");
            foreach (var user in limitedUsers.ToList())
            {
                Console.WriteLine($"User: {user.Id} {user.Gender} {user.ZipCode}");
                Console.WriteLine($"Occupation: {user.Occupation.Name}");
                Console.WriteLine("Rated Movies:");
                foreach (var userMovie in user.UserMovies)
                {
                    Console.WriteLine(userMovie.Movie.Title);
                }
            }
        }
    }

    private static void Main(string[] args)
    {
        //MovieContext db = new MovieContext();
        //db.Users

        //using MovieLibraryEntities.Dao;

        //var repositiry = new Repository();

        var context = new MovieContext();

        var movies = context.Movies;

        //movies.Add(new Movie());

        Console.WriteLine("Enter Movie Name: ");
        var movieName = Console.ReadLine();

        using (var db = new MovieContext())
        {
            var movie = db.Movies.FirstOrDefault(x => x.Title == movieName);
            Console.WriteLine($"({movie.Id}) {movie.Title}");
        }

        //add
        Console.WriteLine("Enter NEW Movie Name: ");
        var newMovie = Console.ReadLine();

        using (var db = new MovieContext())
        {
            var addedMovie = new Movie
            {
                Title = newMovie
            };
            db.Movies.Add(addedMovie);
            db.SaveChanges();

            var movieToAdd = db.Movies.FirstOrDefault(x => x.Title == newMovie);
            Console.WriteLine($"({movieToAdd.Id}) {movieToAdd.Title}");
        }

        //update
        Console.WriteLine("Enter Movie Name to Update: ");
        var movieToUpdate = Console.ReadLine();

        Console.WriteLine("Enter Updated Movie Name: ");
        var movieUpdated = Console.ReadLine();

        using (var db = new MovieContext())
        {
            var updatedMovie = db.Movies.FirstOrDefault(x => x.Title == movieToUpdate);
            Console.WriteLine($"({updatedMovie.Id}) {updatedMovie.Title}");

            updatedMovie.Title = movieUpdated;

            db.Movies.Update(updatedMovie);
            db.SaveChanges();
        }

        // delete
        Console.WriteLine("Enter Movie Name to Delete: ");
        var movieToDelete = Console.ReadLine();

        using (var db = new MovieContext())
        {
            var deletedMovie = db.Movies.FirstOrDefault(x => x.Title == movieToDelete);
            Console.WriteLine($"({deletedMovie.Id}) {deletedMovie.Title}");

            // verify it exists 
            db.Movies.Remove(deletedMovie);
            db.SaveChanges();
        }

        // Display genres for a movie
        using (var db = new MovieContext())
        {
            //var movie = db.Movies
            //.Include(x => x.MovieGenres)
            //.ThenInclude(x => x.Genre)
            //.FirstOrDefault(mov => mov.Title.Contains(Console.Readline));

            Console.WriteLine("Enter movie title: ");
            var title = Console.ReadLine();
            var movie = db.Movies
                .FirstOrDefault(mov => mov.Title.Contains(title));

            Console.WriteLine($"Movie: {movie?.Title} {movie?.ReleaseDate:MM-dd-yyyy}");

            Console.WriteLine("Genres:");

            foreach (var genre in movie?.MovieGenres ?? new List<MovieGenre>())
            {
                Console.WriteLine($"\t{genre.Genre.Name}");
            }
        }
    }
}
