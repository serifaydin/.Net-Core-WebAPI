using Microsoft.EntityFrameworkCore;

namespace Core_WebApp.Models
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {

        }

        public DbSet<Person> Person { get; set; }
    }
}