using MealPlanner_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MealPlanner_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        //passed from container (Program.CS) then pass to base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealIngredients> MealIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>().HasData(
                new Meal()
                {
                    Id = 1,
                    Name = "test",
                    URL = "test",
                    Image = "test",
                    HealthRating = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
                );
        }
    }
}
