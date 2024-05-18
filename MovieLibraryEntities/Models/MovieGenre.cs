namespace MovieLibraryEntities.Models;

public class MovieGenre
{
    public virtual Genre Genre { get; set; }
    public int Id { get; set; }
    public virtual Movie Movie { get; set; }
}
