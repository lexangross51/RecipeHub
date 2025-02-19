using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHub.Domain.Models;

namespace RecipeHub.Persistence.EntityConfigurations;

internal class RecipeStepConfiguration : IEntityTypeConfiguration<RecipeStep>
{
    public void Configure(EntityTypeBuilder<RecipeStep> builder)
    {
        builder.ToTable("RecipeStep");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Description).HasMaxLength(1024);
        builder.HasOne(r => r.Image)
            .WithOne()
            .HasForeignKey("RecipeStep", "ImageId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}