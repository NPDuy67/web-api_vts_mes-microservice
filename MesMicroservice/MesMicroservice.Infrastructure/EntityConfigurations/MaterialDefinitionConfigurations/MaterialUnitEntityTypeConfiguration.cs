using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;

namespace MesMicroservice.Infrastructure.EntityConfigurations.MaterialDefinitionConfigurations;
public class MaterialUnitEntityTypeConfiguration : IEntityTypeConfiguration<MaterialUnit>
{
    public void Configure(EntityTypeBuilder<MaterialUnit> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .UseHiLo("materialuniteq", ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasIndex(x => new { x.UnitId, x.MaterialDefinitionId }).IsUnique();
        builder.Property(x => x.UnitId).HasMaxLength(50).IsRequired();

        builder.Property(x => x.UnitName).HasMaxLength(250).IsRequired();
        builder.Property(x => x.ConversionValueToPrimaryUnit).HasPrecision(30, 8).IsRequired();
    }
}
