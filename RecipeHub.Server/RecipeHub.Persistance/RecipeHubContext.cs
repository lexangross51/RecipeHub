using Microsoft.EntityFrameworkCore;
using RecipeHub.Domain.Models;
using RecipeHub.Persistence.EntityConfigurations;

namespace RecipeHub.Persistence;

internal class RecipeHubContext(DbContextOptions<RecipeHubContext> options) : DbContext(options)
{
    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<RecipeStep> RecipeSteps { get; set; }

    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeStepConfiguration());
        modelBuilder.ApplyConfiguration(new ImageConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}