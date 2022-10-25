using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    //Derive Datacontext class from Entity Framework, that allows query and saving instanses of entities.
    public class DataContext : DbContext
    {
        //Constructor
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        //Create DB set for AppUsers. Uses our own AppUser class from Entities folder.
        public DbSet<AppUser> Users { get; set; }
    }
}