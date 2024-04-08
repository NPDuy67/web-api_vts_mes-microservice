namespace MesMicroservice.Domain.AggregateModels.MaterialLotAggregate;
public class MaterialLot : Resource, IAggregateRoot
{
    public MaterialDefinition MaterialDefinition { get; private set; }
    public decimal Quantity { get; private set; }
    public MaterialUnit Unit { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private MaterialLot() : base() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public MaterialLot(string materialLotId, MaterialDefinition materialDefinition, decimal quantity, MaterialUnit unit): base(materialLotId)
    {
        MaterialDefinition = materialDefinition;
        Quantity = quantity;
        Unit = unit;
    }
}
