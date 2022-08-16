using FloraEducationAPI.Domain.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace FloraEducationAPI.Context
{
    public class FloraEducationDbContext : DbContext
    {
        public FloraEducationDbContext(DbContextOptions<FloraEducationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(e => e.Username);
        }
    }
}
