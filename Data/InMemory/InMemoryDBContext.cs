using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using StitchStack.Models;

namespace StitchStack.Data.InMemory
{
    public class InMemoryDBContext: DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "PatternDB");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Fabric)
                .WithMany(f => f.Projects)
                .HasForeignKey(p => p.FabricId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Pattern)
                .WithMany(f => f.Projects)
                .HasForeignKey(p => p.PatternId)
                .OnDelete(DeleteBehavior.SetNull);
        }
        public InMemoryDBContext(DbContextOptions<InMemoryDBContext> options)
     : base(options)
        { }

        public DbSet<Pattern> Patterns { get; set; }
        public DbSet<Fabric> Fabrics { get; set; }

        public DbSet<Project> Projects { get; set; }
    }
}
