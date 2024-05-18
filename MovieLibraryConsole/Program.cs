using Microsoft.Extensions.Logging;
using MovieLibrary.Services;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Dao;
using MovieLibraryEntities.Models;

namespace MovieApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //MovieContext db = new MovieContext();
            //db.Users




            //using MovieLibraryEntities.Dao;

            //var repositiry = new Repository();



            var context = new MovieContext();

            var movies = context.Movies;

            movies.Add(new Movie() { });



            System.Console.WriteLine("Enter Movie Name: ");
            var movieName = Console.ReadLine();

            using (var db = new MovieContext())
            {
                var occupation = db.Movies.FirstOrDefault(x => x.Title == movieName);
                System.Console.WriteLine($"({Movie.Id}) {Movie.Title}");
            }

            //add
            System.Console.WriteLine("Enter NEW Movie Name: ");
            var newMovie = Console.ReadLine();

            using (var db = new MovieContext())
            {
                var addedMovie = new Movie()
                {
                    Title = newMovie
                };
                db.Movies.Add(Movie);
                db.SaveChanges();

                var movieToAdd = db.Movies.FirstOrDefault(x => x.Title == newMovie);
                System.Console.WriteLine($"({movieToAdd.Id}) {movieToAdd.Title}");
            }

            //update
            System.Console.WriteLine("Enter Movie Name to Update: ");
            var movieToUpdate = Console.ReadLine();

            System.Console.WriteLine("Enter Updated Movie Name: ");
            var movieUpdated = Console.ReadLine();

            using (var db = new MovieContext())
            {
                var updatedMovie = db.Movies.FirstOrDefault(x => x.Name == movieToUpdate);
                System.Console.WriteLine($"({updatedMovie.Id}) {updatedMovie.Name}");

                updatedMovie.Name = movieUpdated;

                db.Movies.Update(updatedMovie);
                db.SaveChanges();

            }

            // delete
            System.Console.WriteLine("Enter Movie Name to Delete: ");
            var movieToDelete = Console.ReadLine();

            using (var db = new MovieContext())
            {
                var deletedMovie = db.Movies.FirstOrDefault(x => x.Name == movieToDelete);
                System.Console.WriteLine($"({deletedMovie.Id}) {deletedMovie.Name}");

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


                var movie = db.Movies
                    .FirstOrDefault(mov => mov.Title.Contains(Console.Readline));

                System.Console.WriteLine($"Movie: {movie?.Title} {movie?.ReleaseDate:MM-dd-yyyy}");

                System.Console.WriteLine("Genres:");

                foreach (var genre in movie?.MovieGenres ?? new List<MovieGenre>())
                {
                    System.Console.WriteLine($"\t{genre.Genre.Name}");
                }
            }
        }





        private readonly ILogger<IFileService> _logger;
        private readonly string _file;

        public FileService(ILogger<IFileService> logger)
        {
            _logger = logger;
            _file = $"{Environment.CurrentDirectory}/Files/movie.cs";
        }

        // Additional constructor for testing
        public FileService(ILogger<IFileService> logger, string filePath)
        {
            _logger = logger;
            _file = filePath;
        }

        public void Invoke()
        {
            string choice;
            do
            {

                // Input genres
                List<string> genres = new List<string>();
                string genre;
                do
                {
                    // Ask user to enter genre
                    Console.WriteLine("Enter genre (or done to quit)");
                    genre = Console.ReadLine();
                    // If user enters "done" or does not enter a genre do not add it to list
                    if (genre != "done" && genre.Length > 0)
                    {
                        genres.Add(genre);
                    }
                } while (genre != "done");



                _fileService.Read();



            }
            while (choice != "X");




            // Add userMovie
            using (var db = new MovieContext())
            {
                // build user object (not database)
                var user = db.Users.FirstOrDefault(u => u.Id == 943);
                var movie = db.Movies.FirstOrDefault(m => m.Title.Contains(Console.ReadLine));

                // build user/movie relationship object (not database)
                var userMovie = new UserMovie()
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
                    Console.WriteLine($"Rated Movies:");
                    foreach (var userMovie in user.UserMovies)
                    {
                        Console.WriteLine(userMovie.Movie.Title);
                    }
                }
            }

        }
    }
}