using FloraEducationAPI.Domain.Models;
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
        public virtual DbSet<Plant> Plants { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Defining Primary Keys
            modelBuilder
                .Entity<User>()
                .HasKey(e => e.Username);

            modelBuilder
                .Entity<Plant>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<Comment>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<MiniQuiz>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<QuizQuestion>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();


            // Defining Foreign Keys
            modelBuilder
                .Entity<Comment>()
                .HasOne(e => e.Plant);

            modelBuilder
                .Entity<Comment>()
                .HasOne(e => e.Author);

            modelBuilder
                .Entity<MiniQuiz>()
                .HasOne(e => e.Plant);

            modelBuilder
                .Entity<QuizQuestion>()
                .HasOne(e => e.Quiz)
                .WithMany(e => e.Questions);
        }
    }
}
