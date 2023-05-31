using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Webb_MovieShop.Models;

namespace Webb_MovieShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Webb_MovieShop.Models.Movie> Movie { get; set; } = default!;
        public DbSet<Webb_MovieShop.Models.Snack> Snack { get; set; } = default!;
    }
}