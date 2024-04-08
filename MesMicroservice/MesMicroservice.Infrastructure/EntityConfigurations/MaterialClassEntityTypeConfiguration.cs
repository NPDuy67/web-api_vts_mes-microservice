using MesMicroservice.Domain.AggregateModels.MaterialClassAggregate;

namespace MesMicroservice.Infrastructure.EntityConfigurations;

public class MaterialClassEntityTypeConfiguration : IEntityTypeConfiguration<MaterialClass>
{
    public void Configure(EntityTypeBuilder<MaterialClass> builder)
    {
        builder.ToTable(nameof(MaterialClass));

        builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
    }
}
