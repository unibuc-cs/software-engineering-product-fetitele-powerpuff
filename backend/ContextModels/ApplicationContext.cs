using healthy_lifestyle_web_app.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.ContextModels
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>().HasKey(d => new { d.ProfileId, d.Date });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DayFood>().HasKey(d => new { d.ProfileId, d.Date, d.FoodId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DayPhysicalActivity>().HasKey(d => new { d.ProfileId, d.Date, d.PhysicalActivityId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WeightEvolution>().HasKey(we => new { we.EvolutionId, we.ProfileId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecipeFood>().HasKey(rf => new { rf.RecipeId, rf.FoodId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Muscle>()
               .HasIndex(m => m.Name)
               .IsUnique();

            modelBuilder.Entity<PhysicalActivity>()
               .HasIndex(p => p.Name)
               .IsUnique();

            modelBuilder.Entity<Food>()
               .HasIndex(m => m.Name)
               .IsUnique();

            modelBuilder.Entity<Recipe>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Article>()
           .HasIndex(a => a.Title)
           .IsUnique();
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<PhysicalActivity> PhysicalActivities { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<DayFood> DayFoods { get; set; }
        public DbSet<DayPhysicalActivity> DayPhysicalActivities { get; set; }
        public DbSet<WeightEvolution> WeightEvolutions { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeFood> RecipeFoods { get; set; }
        public DbSet<Article> Articles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}