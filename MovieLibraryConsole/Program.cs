using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;

namespace MovieApp
{
    internal class Program
    {
        private readonly ILogger<Program> _logger;

        public Program(ILogger<Program> logger)
        {
            _logger = logger;
        }

        public void AddMovie(MovieContext db)
        {
            Console.WriteLine("Enter the title of the movie to add:");
            var title = Console.ReadLine();
            var newMovie = new Movie { Title = title };
            db.Movies.Add(newMovie);
            db.SaveChanges();
            _logger.LogInformation($"Added new movie: {title}");
        }

        public void AddUser(MovieContext db)
        {
            Console.WriteLine("Enter user details:");
            Console.WriteLine("Name:");
            var name = Console.ReadLine();
            Console.WriteLine("Age:");
            var age = int.Parse(Console.ReadLine()); // You should add error handling for invalid inputs
            Console.WriteLine("Occupation:");
            var occupationName = Console.ReadLine();

            // Check if the occupation already exists
            var occupation = db.Occupations.FirstOrDefault(o => o.Name == occupationName);
            if (occupation == null)
            {
                // If the occupation doesn't exist, create a new one
                occupation = new Occupation { Name = occupationName };
                db.Occupations.Add(occupation);
                db.SaveChanges();
            }

            // Create a new user and associate it with the occupation


            var newUser = new User
            {
                Name = name,
                Age = age,
                OccupationId = (int)occupation.Id
            };
            db.Users.Add(newUser);
            db.SaveChanges();

            _logger.LogInformation($"Added new user: {name}");

            //    var newUser = new User
            //    {
            //        Name = name,
            //        Age = age,
            //        OccupationId = (int)occupation.Id
            //    };
            //    db.Users.Add(newUser);
            //    db.SaveChanges();

            //    _logger.LogInformation($"Added new user: {name}");
            //}
        }
            public void EditMovie(MovieContext db)
            {
                Console.WriteLine("Enter the title of the movie to edit:");
                var title = Console.ReadLine();
                var movie = db.Movies.FirstOrDefault(m => m.Title == title);
                if (movie != null)
                {
                    Console.WriteLine("Enter the new title for the movie:");
                    var newTitle = Console.ReadLine();
                    movie.Title = newTitle;
                    db.SaveChanges();
                    _logger.LogInformation($"Edited movie '{title}' to '{newTitle}'");
                }
                else
                {
                    Console.WriteLine($"Movie '{title}' not found.");
                }
            }

            public void DisplayMovies(MovieContext db)
            {
                var movies = db.Movies.ToList();
                Console.WriteLine("All movies:");
                foreach (var movie in movies)
                {
                    Console.WriteLine($"Title: {movie.Title}");
                    // Display genres alongside movie title
                    Console.WriteLine("Genres:");

                    if (movie.MovieGenres != null && movie.MovieGenres.Any())
                    {
                        foreach (var movieGenre in movie.MovieGenres.Select(mg => mg.Genre.Name))
                        {
                            Console.WriteLine($"\t{movieGenre}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tNo genres found.");
                    }
                    Console.WriteLine();
                }
                _logger.LogInformation($"Displayed all {movies.Count} movies.");
            }

            public void SearchMovie(MovieContext db)
            {
                Console.WriteLine("Enter the title of the movie to search:");
                var title = Console.ReadLine();
                var movie = db.Movies.FirstOrDefault(m => m.Title.Contains(title));
                if (movie != null)
                {
                    Console.WriteLine($"Title: {movie.Title}");
                    // Display genres alongside movie title
                    Console.WriteLine("Genres:");
                    if (movie.MovieGenres != null)
                    {
                        foreach (var genre in movie.MovieGenres.Select(mg => mg.Genre.Name))
                        {
                            Console.WriteLine($"\t{genre}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tNo genres found.");
                    }
                }
                else
                {
                    Console.WriteLine($"Movie '{title}' not found.");
                }
                _logger.LogInformation($"Searched for movie: {title}");
            }

            public void Run()
            {
                using (var db = new MovieContext())
                {
                    while (true)
                    {
                        Console.WriteLine("Choose an option:");
                        Console.WriteLine("1. Add Movie");
                        Console.WriteLine("2. Edit Movie");
                        Console.WriteLine("3. Display All Movies");
                        Console.WriteLine("4. Search Movie");
                        Console.WriteLine("5. Add User");
                        Console.WriteLine("6. Rate Movie");
                        Console.WriteLine("7. Display User Rated Movies");
                        Console.WriteLine("8. Exit");

                        var choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                AddMovie(db);
                                break;
                            case "2":
                                EditMovie(db);
                                break;
                            case "3":
                                DisplayMovies(db);
                                break;
                            case "4":
                                SearchMovie(db);
                                break;
                            case "5":
                                AddUser(db);
                                break;
                            case "6":
                                // Implement RateMovie(db);
                                Console.WriteLine("Rate Movie option is not implemented yet.");
                                break;
                            case "7":
                                // Implement DisplayUserRatedMovies(db);
                                Console.WriteLine("Display User Rated Movies option is not implemented yet.");
                                break;
                            case "8":
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                }
            }

            private static void Main(string[] args)
            {
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<Program>();
                var program = new Program(logger);
                program.Run();
            }
        }
    }










//using System;
//using System.Linq;
//using Microsoft.Extensions.Logging;
//using MovieLibraryEntities.Context;
//using MovieLibraryEntities.Models;

//namespace MovieApp
//{
//    internal class Program
//    {
//        private readonly ILogger<Program> _logger;

//        public Program(ILogger<Program> logger)
//        {
//            _logger = logger;
//        }

//        public void AddMovie(MovieContext db)
//        {
//            Console.WriteLine("Enter the title of the movie to add:");
//            var title = Console.ReadLine();
//            var newMovie = new Movie { Title = title };
//            db.Movies.Add(newMovie);
//            db.SaveChanges();
//            _logger.LogInformation($"Added new movie: {title}");
//        }
//        public void AddUser(MovieContext db)
//        {
//            Console.WriteLine("Enter user details:");
//            Console.WriteLine("Name:");
//            var name = Console.ReadLine();
//            Console.WriteLine("Age:");
//            var age = int.Parse(Console.ReadLine()); // You should add error handling for invalid inputs
//            Console.WriteLine("Occupation:");
//            var occupationName = Console.ReadLine();

//            // Check if the occupation already exists
//            var occupation = db.Occupations.FirstOrDefault(o => o.Name == occupationName);
//            if (occupation == null)
//            {
//                // If the occupation doesn't exist, create a new one
//                occupation = new Occupation { Name = occupationName };
//                db.Occupations.Add(occupation);
//                db.SaveChanges();
//            }

//            // Create a new user and associate it with the occupation
//            var newUser = new User
//            {
//                Name = name,
//                Age = age,
//                OccupationId = occupation.Id
//            };
//            db.Users.Add(newUser);
//            db.SaveChanges();

//            _logger.LogInformation($"Added new user: {name}");
//        }

//        public void EditMovie(MovieContext db)
//        {
//            Console.WriteLine("Enter the title of the movie to edit:");
//            var title = Console.ReadLine();
//            var movie = db.Movies.FirstOrDefault(m => m.Title == title);
//            if (movie != null)
//            {
//                Console.WriteLine("Enter the new title for the movie:");
//                var newTitle = Console.ReadLine();
//                movie.Title = newTitle;
//                db.SaveChanges();
//                _logger.LogInformation($"Edited movie '{title}' to '{newTitle}'");
//            }
//            else
//            {
//                Console.WriteLine($"Movie '{title}' not found.");
//            }
//        }

//        public void DisplayMovies(MovieContext db)
//        {
//            var movies = db.Movies.ToList();
//            Console.WriteLine("All movies:");
//            foreach (var movie in movies)
//            {
//                Console.WriteLine($"Title: {movie.Title}");
//                // Display genres alongside movie title
//                Console.WriteLine("Genres:");

//                if (movie.MovieGenres != null && movie.MovieGenres.Any())
//                {
//                    foreach (var movieGenre in movie.MovieGenres.Select(mg => mg.Genre.Name))
//                    {
//                        Console.WriteLine($"\t{movieGenre}");
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("\tNo genres found.");
//                }
//                Console.WriteLine();
//            }
//            _logger.LogInformation($"Displayed all {movies.Count} movies.");
//        }

//        public void SearchMovie(MovieContext db)
//        {
//            Console.WriteLine("Enter the title of the movie to search:");
//            var title = Console.ReadLine();
//            var movie = db.Movies.FirstOrDefault(m => m.Title.Contains(title));
//            if (movie != null)
//            {
//                Console.WriteLine($"Title: {movie.Title}");
//                // Display genres alongside movie title
//                Console.WriteLine("Genres:");
//                if (movie.MovieGenres != null)
//                {
//                    foreach (var genre in movie.MovieGenres.Select(mg => mg.Genre.Name))
//                    {
//                        Console.WriteLine($"\t{genre}");
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("\tNo genres found.");
//                }
//            }
//            else
//            {
//                Console.WriteLine($"Movie '{title}' not found.");
//            }
//            _logger.LogInformation($"Searched for movie: {title}");
//        }

//        public void Run()
//        {
//            using (var db = new MovieContext())
//            {
//                while (true)
//                {
//                    Console.WriteLine("Choose an option:");
//                    Console.WriteLine("1. Add Movie");
//                    Console.WriteLine("2. Edit Movie");
//                    Console.WriteLine("3. Display All Movies");
//                    Console.WriteLine("4. Search Movie");
//                    Console.WriteLine("5. Add User");
//                    Console.WriteLine("6. Rate Movie");
//                    Console.WriteLine("7. Display User Rated Movies");
//                    Console.WriteLine("8. Exit");

//                    var choice = Console.ReadLine();

//                    switch (choice)
//                    {
//                        case "1":
//                            AddMovie(db);
//                            break;
//                        case "2":
//                            EditMovie(db);
//                            break;
//                        case "3":
//                            DisplayMovies(db);
//                            break;
//                        case "4":
//                            SearchMovie(db);
//                            break;
//                        case "5":
//                            AddUser(db);
//                            break;
//                        case "6":
//                            RateMovie(db);
//                            break;
//                        case "7":
//                            DisplayUserRatedMovies(db);
//                            break;
//                        case "8":
//                            return;
//                        default:
//                            Console.WriteLine("Invalid choice. Please try again.");
//                            break;
//                    }
//                }
//            }
//        }



//        private static void Main(string[] args)
//        {
//            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
//            var logger = loggerFactory.CreateLogger<Program>();
//            var program = new Program(logger);
//            program.Run();
//        }
//    }
//}




//using System;
//using System.Linq;
//using Microsoft.Extensions.Logging;
//using MovieLibraryEntities.Context;
//using MovieLibraryEntities.Models;

//namespace MovieApp
//{
//    internal class Program
//    {
//        private readonly ILogger<Program> _logger;

//        public Program(ILogger<Program> logger)
//        {
//            _logger = logger;
//        }

//        public void AddMovie(MovieContext db)
//        {
//            Console.WriteLine("Enter the title of the movie to add:");
//            var title = Console.ReadLine();
//            var newMovie = new Movie { Title = title };
//            db.Movies.Add(newMovie);
//            db.SaveChanges();
//            _logger.LogInformation($"Added new movie: {title}");
//        }

//        public void EditMovie(MovieContext db)
//        {
//            Console.WriteLine("Enter the title of the movie to edit:");
//            var title = Console.ReadLine();
//            var movie = db.Movies.FirstOrDefault(m => m.Title == title);
//            if (movie != null)
//            {
//                Console.WriteLine("Enter the new title for the movie:");
//                var newTitle = Console.ReadLine();
//                movie.Title = newTitle;
//                db.SaveChanges();
//                _logger.LogInformation($"Edited movie '{title}' to '{newTitle}'");
//            }
//            else
//            {
//                Console.WriteLine($"Movie '{title}' not found.");
//            }
//        }
//        //Console.writeline("Here");

//        public void DisplayMovies(MovieContext db)
//        {
//            var movies = db.Movies.ToList();
//            Console.WriteLine("All movies:");
//            foreach (var movie in movies)
//            {
//                Console.WriteLine($"Title: {movie.Title}");
//                // Display genres alongside movie title
//                Console.WriteLine("Genres:");
//                //var genre = " ";
//                foreach ( var genre in movie.MovieGenres.Select(mg => mg.Genre.Name))
//                {
//                    Console.WriteLine($"\t{genre}");
//                }
//                Console.WriteLine();
//            }
//            _logger.LogInformation($"Displayed all {movies.Count} movies.");
//        }

//        public void SearchMovie(MovieContext db)
//        {
//            Console.WriteLine("Enter the title of the movie to search:");
//            var title = Console.ReadLine();
//            var movie = db.Movies.FirstOrDefault(m => m.Title.Contains(title));
//            if (movie != null)
//            {
//                Console.WriteLine($"Title: {movie.Title}");
//                // Display genres alongside movie title
//                Console.WriteLine("Genres:");
//                foreach (var genre in movie.MovieGenres.Select(mg => mg.Genre.Name))
//                {
//                    Console.WriteLine($"\t{genre}");
//                }
//            }
//            else
//            {
//                Console.WriteLine($"Movie '{title}' not found.");
//            }
//            _logger.LogInformation($"Searched for movie: {title}");
//        }

//        public void Run()
//        {
//            using (var db = new MovieContext())
//            {
//                while (true)
//                {
//                    Console.WriteLine("Choose an option:");
//                    Console.WriteLine("1. Add Movie");
//                    Console.WriteLine("2. Edit Movie");
//                    Console.WriteLine("3. Display All Movies");
//                    Console.WriteLine("4. Search Movie");
//                    Console.WriteLine("5. Exit");

//                    var choice = Console.ReadLine();

//                    switch (choice)
//                    {
//                        case "1":
//                            AddMovie(db);
//                            break;
//                        case "2":
//                            EditMovie(db);
//                            break;
//                        case "3":
//                            DisplayMovies(db);
//                            break;
//                        case "4":
//                            SearchMovie(db);
//                            break;
//                        case "5":
//                            return;
//                        default:
//                            Console.WriteLine("Invalid choice. Please try again.");
//                            break;
//                    }
//                }
//            }
//        }

//        private static void Main(string[] args)
//        {
//            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
//            var logger = loggerFactory.CreateLogger<Program>();

//            var program = new Program(logger);
//            program.Run();
//        }
//    }
//}
















//using System;
//using System.Linq;
//using Microsoft.Extensions.Logging;
//using MovieLibraryEntities.Context;
//using MovieLibraryEntities.Models;

//namespace MovieApp;

//internal class Program
//{
//    private readonly string _file;

//    private readonly ILogger<Program> _logger;

//    public Program(ILogger<Program> logger)
//    {
//        _logger = logger;
//        _file = $"{Environment.CurrentDirectory}/Files/movie.cs";
//    }

//    // Additional constructor for testing
//    public Program(ILogger<Program> logger, string filePath)
//    {
//        _logger = logger;
//        _file = filePath;
//    }

//    public void Invoke()
//    {
//        string genre;
//        do
//        {
//            // Input genres
//            var genres = new List<string>();
//            do
//            {
//                // Ask user to enter genre
//                Console.WriteLine("Enter genre (or X to quit)");
//                genre = Console.ReadLine();
//                // If user enters "done" or does not enter a genre do not add it to list
//                if (genre != "done" && genre.Length > 0)
//                {
//                    genres.Add(genre);
//                }
//            }
//            while (genre != "done");

//            //_fileService.Read(); 
//        }
//        while (genre != "X");

//        // Add userMovie
//        using (var db = new MovieContext())
//        {
//            // build user object (not database)1
//            Console.WriteLine("Enter movie title:");
//            var title = Console.ReadLine();
//            var user = db.Users.FirstOrDefault(u => u.Id == 943);
//            var movie = db.Movies.FirstOrDefault(m => m.Title.Contains(title));

//            // build user/movie relationship object (not database)
//            var userMovie = new UserMovie
//            {
//                Rating = 2,
//                RatedAt = DateTime.Now
//            };

//            // set up the database relationships
//            userMovie.User = user;
//            userMovie.Movie = movie;

//            // db.Users.Add(user);
//            db.UserMovies.Add(userMovie);

//            // commit
//            db.SaveChanges();
//        }

//        // Get user movie rating details
//        using (var db = new MovieContext())
//        {
//            var users = db.Users;
//            //.Include(x => x.Occupation)
//            //.Include(x => x.UserMovies)
//            //.ThenInclude(x => x.Movie);

//            var limitedUsers = users.Take(10);

//            Console.WriteLine("The users are :");
//            foreach (var user in limitedUsers.ToList())
//            {
//                Console.WriteLine($"User: {user.Id} {user.Gender} {user.ZipCode}");
//                Console.WriteLine($"Occupation: {user.Occupation.Name}");
//                Console.WriteLine("Rated Movies:");
//                foreach (var userMovie in user.UserMovies)
//                {
//                    Console.WriteLine(userMovie.Movie.Title);
//                }
//            }
//        }
//    }

//    private static void Main(string[] args)
//    {
//        //MovieContext db = new MovieContext();
//        //db.Users

//        //using MovieLibraryEntities.Dao;

//        //var repositiry = new Repository();

//        var context = new MovieContext();

//        var movies = context.Movies;

//        //movies.Add(new Movie());

//        Console.WriteLine("Enter Movie Name: ");
//        var movieName = Console.ReadLine();

//        using (var db = new MovieContext())
//        {
//            var movie = db.Movies.FirstOrDefault(x => x.Title == movieName);
//            Console.WriteLine($"({movie.Id}) {movie.Title}");
//            //Console.WriteLine($"({movie.Title}");
//        }

//        //add
//        Console.WriteLine("Enter NEW Movie Name: ");
//        var newMovie = Console.ReadLine();

//        using (var db = new MovieContext())
//        {
//            var addedMovie = new Movie
//            {
//                Title = newMovie
//            };
//            db.Movies.Add(addedMovie);
//            db.SaveChanges();

//            var movieToAdd = db.Movies.FirstOrDefault(x => x.Title == newMovie);
//            Console.WriteLine($"({movieToAdd.Id}) {movieToAdd.Title}");
//        }

//        //update
//        Console.WriteLine("Enter Movie Name to Update: ");
//        var movieToUpdate = Console.ReadLine();

//        Console.WriteLine("Enter Updated Movie Name: ");
//        var movieUpdated = Console.ReadLine();

//        using (var db = new MovieContext())
//        {
//            var updatedMovie = db.Movies.FirstOrDefault(x => x.Title == movieToUpdate);
//            Console.WriteLine($"({updatedMovie.Id}) {updatedMovie.Title}");

//            updatedMovie.Title = movieUpdated;

//            db.Movies.Update(updatedMovie);
//            db.SaveChanges();
//        }

//        // delete
//        Console.WriteLine("Enter Movie Name to Delete: ");
//        var movieToDelete = Console.ReadLine();

//        using (var db = new MovieContext())
//        {
//            var deletedMovie = db.Movies.FirstOrDefault(x => x.Title == movieToDelete);
//            Console.WriteLine($"({deletedMovie.Id}) {deletedMovie.Title}");

//            // verify it exists 
//            db.Movies.Remove(deletedMovie);
//            db.SaveChanges();
//        }

//        // Display genres for a movie
//        using (var db = new MovieContext())
//        {
//            //var movie = db.Movies
//            //.Include(x => x.MovieGenres)
//            //.ThenInclude(x => x.Genre)
//            //.FirstOrDefault(mov => mov.Title.Contains(Console.Readline));

//            Console.WriteLine("Enter movie title: ");
//            var title = Console.ReadLine();
//            var movie = db.Movies
//                .FirstOrDefault(mov => mov.Title.Contains(title));

//            Console.WriteLine($"Movie: {movie?.Title} {movie?.ReleaseDate:MM-dd-yyyy}");

//            Console.WriteLine("Genres:");

//            foreach (var genre in movie?.MovieGenres ?? new List<MovieGenre>())
//            {
//                Console.WriteLine($"\t{genre.Genre.Name}");
//            }
//        }
//    }
//}




