using Microsoft.EntityFrameworkCore;

namespace FloraEducationAPI.Context
{
    public class FloraEducationDbContext : DbContext
    {
        public FloraEducationDbContext(DbContextOptions<FloraEducationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
