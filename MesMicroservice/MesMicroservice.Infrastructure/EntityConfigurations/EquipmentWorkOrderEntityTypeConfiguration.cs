using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Infrastructure.EntityConfigurations;
public class EquipmentWorkOrderEntityTypeConfiguration : IEntityTypeConfiguration<EquipmentWorkOrder>
{
    public void Configure(EntityTypeBuilder<EquipmentWorkOrder> builder)
    {
        builder.HasKey(x => new { x.WorkOrderId, x.EquipmentId });

        builder.HasOne(x => x.Equipment).WithMany().HasForeignKey(x => x.WorkOrderId).IsRequired().OnDelete(DeleteBehavior.NoAction);
    }
}
