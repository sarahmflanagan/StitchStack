using Microsoft.EntityFrameworkCore;
using StitchStack.Data.InMemory;
using StitchStack.Models;

namespace StitchStack.Data.SqlDB
{
    public class SqlDBContext(DbContextOptions<SqlDBContext> options) : DbContext(options)
    {
        public DbSet<Pattern> Patterns { get; set; }
        public DbSet<Fabric> Fabrics { get; set; }

        public DbSet<Project> Projects { get; set; }
    }
}