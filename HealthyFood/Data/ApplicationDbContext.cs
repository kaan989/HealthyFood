using HealthyFood.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthyFood.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeCategory>()
                 .HasKey(rc => new { rc.RecipeId, rc.CategoryId });
            modelBuilder.Entity<RecipeCategory>()
                 .HasOne(a => a.Recipe)
                 .WithMany(ab => ab.RecipeCategories)
                 .HasForeignKey(c => c.RecipeId);
            modelBuilder.Entity<RecipeCategory>()
                 .HasOne(b => b.Category)
                 .WithMany(ba => ba.RecipeCategories)
                 .HasForeignKey(ab => ab.CategoryId);
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
