using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,string>
    {

        public DbSet<City> Cities { get; set; }
        public DbSet<House> House { get; set; } 

        public DbSet<Contact> Contacts { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
