using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHub.Domain.Models;

namespace RecipeHub.Persistence.EntityConfigurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id);
        builder.HasIndex(p => p.Name);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(128);
        builder.HasOne(p => p.Image)
            .WithOne()
            .HasForeignKey("Product", "ImageId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}