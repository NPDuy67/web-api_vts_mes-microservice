using MesMicroservice.Domain.AggregateModels.MaterialLotAggregate;

namespace MesMicroservice.Infrastructure.EntityConfigurations;
public class MaterialLotEntityTypeConfiguration : IEntityTypeConfiguration<MaterialLot>
{
    public void Configure(EntityTypeBuilder<MaterialLot> builder)
    {
        builder.ToTable(nameof(MaterialLot));

        builder.HasOne(x => x.Unit).WithMany();
        builder.HasOne(x => x.MaterialDefinition).WithMany(x => x.MaterialLots).IsRequired().OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Quantity).HasPrecision(30, 8).IsRequired();
    }
}
