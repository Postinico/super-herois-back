using API.Models;

using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Heroi> Heroi { get; set; }

        public DbSet<HeroiSuperpoder> HeroiSuperpoder { get; set; }

        public DbSet<Superpoder> Superpoder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
