using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHub.Domain.Models;

namespace RecipeHub.Persistence.EntityConfigurations;

internal class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.OwnsOne(i => i.Measure, measure =>
        {
            measure.Property(m => m.Unit).HasMaxLength(8).IsRequired();
            measure.Property(m => m.Value).IsRequired();
        });
    }
}