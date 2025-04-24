using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository.Context
{
    public partial class GenericContext : DbContext
    {
        public GenericContext()
        {
        }

        public GenericContext(DbContextOptions<GenericContext> options) : base(options)
        {
        }

        public DbSet<Generic> Generics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Generic>().ToTable("Generics");
            modelBuilder.Entity<Generic>().HasKey(g => g.Id);
            modelBuilder.Entity<Generic>().Property(g => g.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Generic>().Property(g => g.Description).HasMaxLength(500);
        }
    }
}
