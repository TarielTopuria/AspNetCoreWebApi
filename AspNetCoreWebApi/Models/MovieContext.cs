using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApi.Models
{
    //The database context is the main class that coordinates Entity Framework functionality for a data model. This class is created by deriving from the Microsoft.EntityFrameworkCore.DbContext class.
    public class MovieContext : DbContext
    {
        //The name of the connection string is passed into the context by calling a method on a DbContextOptions object. For local development, the ASP.NET Core configuration system reads the connection string from the appsettings.json file.
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } //The preceding code creates a DbSet<Movie> property for the entity set.

        //In Entity Framework terminology, an entity set typically corresponds to a database table and an entity corresponds to a row in the table.
    }
}
