using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Infrastructure.EntityConfigurations;
public class WorkOrderEntityTypeConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .UseHiLo("workordereq", ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasIndex(x => new { x.ManufacturingOrderId, x.WorkOrderId}).IsUnique();

        builder.Property(x => x.WorkOrderId).HasMaxLength(50).IsRequired();

        builder.HasMany(x => x.PrerequisiteOperations).WithMany();
        builder.HasOne(x => x.WorkCenter).WithMany();

        builder.Property(x => x.Duration)
            .HasConversion(
                x => x.TotalMilliseconds,
                x => TimeSpan.FromMilliseconds(x));

        builder.Property(x => x.ProductionTarget).HasPrecision(30, 8).IsRequired();
    }
}
