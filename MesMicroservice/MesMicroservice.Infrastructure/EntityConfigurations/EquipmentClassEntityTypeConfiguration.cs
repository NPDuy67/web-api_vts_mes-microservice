using MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;

namespace MesMicroservice.Infrastructure.EntityConfigurations;
public class EquipmentClassEntityTypeConfiguration : IEntityTypeConfiguration<EquipmentClass>
{
    public void Configure(EntityTypeBuilder<EquipmentClass> builder)
    {
        builder.ToTable(nameof(EquipmentClass));

        builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
    }
}
