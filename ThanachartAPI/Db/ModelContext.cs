using Microsoft.EntityFrameworkCore;
using ThanachartAPI.Db.Entity;

namespace ThanachartAPI.Db
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
        public virtual DbSet<Product> Products { get; set; }
    }
}
