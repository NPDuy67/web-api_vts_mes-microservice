using MesMicroservice.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;

namespace MesMicroservice.Infrastructure.EntityConfigurations;
public class ResourceEntityTypeConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .UseHiLo("resourceeq", ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasIndex(x => x.ResourceId).IsUnique();
        builder.Property(x => x.ResourceId).HasMaxLength(50).IsRequired();
    }
}
