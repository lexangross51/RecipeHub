using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHub.Domain.Models;

namespace RecipeHub.Persistence.EntityConfigurations;

internal class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Id);
        builder.HasIndex(e => e.Name);
        builder.Property(e => e.Name)
            .HasMaxLength(256)
            .IsRequired();
        builder.HasOne(e => e.RecipeImage)
            .WithOne()
            .HasForeignKey("Recipe", "ImageId")
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(e => e.Ingredients)
            .WithOne()
            .HasForeignKey("RecipeId")
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(e => e.Steps)
            .WithOne()
            .HasForeignKey(e => e.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}