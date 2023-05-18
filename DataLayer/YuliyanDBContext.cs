using BussinesLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class YuliyanDBContext : DbContext
    {
        public YuliyanDBContext()
        {

        }
        public YuliyanDBContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-GK4F9OM\\SQLEXPRESS;Database=YuliyanDB;Trusted_Connection=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
