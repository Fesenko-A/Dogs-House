using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.EntityFramework.Data {
    internal class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<DogEntity> Dogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DogEntity>(entity => { 
                entity.HasKey(entity => entity.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Color).IsRequired();
            });
        }
    }
}
