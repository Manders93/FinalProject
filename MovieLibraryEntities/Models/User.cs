namespace MovieLibraryEntities.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int OccupationId { get; set; } // Change type to int
    public Occupation Occupation { get; set; }
}
//public long Age { get; set; }
//public string Gender { get; set; }
//public long Id { get; set; }

//public virtual Occupation Occupation { get; set; }
//public virtual ICollection<UserMovie> UserMovies { get; set; }
//public string ZipCode { get; set; }
