using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;

namespace MesMicroservice.Infrastructure.EntityConfigurations.MaterialDefinitionConfigurations;
public class OperationEquipmentRequirementEntityTypeConfiguration : IEntityTypeConfiguration<OperationEquipmentRequirement>
{
    public void Configure(EntityTypeBuilder<OperationEquipmentRequirement> builder)
    {
        builder.HasKey(x => new { x.OperationId, x.EquipmentClassId });

        builder
            .HasOne(x => x.EquipmentClass)
            .WithMany()
            .IsRequired()
            .HasForeignKey(x => x.EquipmentClassId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
