using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;

namespace MesMicroservice.Infrastructure.EntityConfigurations;
public class ManufacturingOrderEntityTypeConfiguration : IEntityTypeConfiguration<ManufacturingOrder>
{
    public void Configure(EntityTypeBuilder<ManufacturingOrder> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .UseHiLo("manufacturingordereq", ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasIndex(x => x.ManufacturingOrderId).IsUnique();
        builder.Property(x => x.ManufacturingOrderId).HasMaxLength(50).IsRequired();

        builder.HasOne(x => x.MaterialDefinition).WithMany();
        builder.HasMany(x => x.WorkOrders).WithOne(x => x.ManufacturingOrder).HasForeignKey(x => x.ManufacturingOrderId);

        builder.Property(x => x.Quantity).HasPrecision(30, 8).IsRequired();
        builder.Property(x => x.Unit).IsRequired();
        builder.Property(x => x.DueDate).IsRequired();
        builder.Property(x => x.AvailableDate).IsRequired();
        builder.Property(x => x.Priority).IsRequired();
    }
}
