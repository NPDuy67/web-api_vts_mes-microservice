using MesMicroservice.Domain.AggregateModels.EquipmentAggregate;

namespace MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;
public class EquipmentClass : Resource, IAggregateRoot
{
    public string Name { get; private set; }
    public List<Equipment> Equipments { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EquipmentClass() : base() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public EquipmentClass(string equipmentClassId, string name): base(equipmentClassId)
    {
        Name = name;
        Equipments = new List<Equipment>();
    }

    public void Update(string name)
    {
        Name = name;
    }
}
