using MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;

namespace MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;
public class OperationEquipmentRequirement
{
    public int OperationId { get; private set; }
    public int EquipmentClassId { get; private set; }
    public EquipmentClass EquipmentClass { get; private set; }
    public int Quantity { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private OperationEquipmentRequirement() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public OperationEquipmentRequirement(EquipmentClass equipmentClass, int quantity)
    {
        EquipmentClass = equipmentClass;
        Quantity = quantity;
    }
}
