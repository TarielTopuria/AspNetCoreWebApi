namespace AspNetCoreWebApi.Models
{
    //EF Core is an object-relational mapping (ORM) framework that simplifies the data access code. Model classes don’t have any dependency on EF Core. They just define the properties of the data that will be stored in the database.
    public class Movie
    {
        public int Id { get; set; } //The Id field is required by the database for the primary key.
        public string? Title { get; set; }
        public string? Genre{ get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
