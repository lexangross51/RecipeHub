using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHub.Domain.Models;

namespace RecipeHub.Persistence.EntityConfigurations;

internal class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(i => i.Id);
        builder.OwnsOne(i => i.Path, path =>
        {
            path.Property(p => p.Server).IsRequired().HasMaxLength(32);
            path.Property(p => p.PathLocation).IsRequired().HasMaxLength(256);
        });
        builder.Ignore(i => i.Data);
        builder.Ignore(i => i.Name);
    }
}