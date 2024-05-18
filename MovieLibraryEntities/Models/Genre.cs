namespace MovieLibraryEntities.Models;

public class Genre
{
    public long Id { get; set; }

    public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    public string Name { get; set; }
}
