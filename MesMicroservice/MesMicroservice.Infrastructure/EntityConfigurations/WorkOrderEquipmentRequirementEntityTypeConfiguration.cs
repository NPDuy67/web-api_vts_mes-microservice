using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Infrastructure.EntityConfigurations;
public class WorkOrderEquipmentRequirementEntityTypeConfiguration : IEntityTypeConfiguration<WorkOrderEquipmentRequirement>
{
    public void Configure(EntityTypeBuilder<WorkOrderEquipmentRequirement> builder)
    {
        builder.HasKey(x => new { x.WorkOrderId, x.EquipmentClassId });

        builder
            .HasOne(x => x.EquipmentClass)
            .WithMany()
            .IsRequired()
            .HasForeignKey(x => x.EquipmentClassId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
