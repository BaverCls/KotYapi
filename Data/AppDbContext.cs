using Microsoft.EntityFrameworkCore;
using Proje.Models;

namespace Proje.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}