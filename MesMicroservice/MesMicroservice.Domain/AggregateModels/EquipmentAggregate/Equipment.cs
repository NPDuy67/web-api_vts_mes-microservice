using MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;

namespace MesMicroservice.Domain.AggregateModels.EquipmentAggregate;
public class Equipment : Resource, IAggregateRoot
{
    public string Name { get; private set; }
    public List<EquipmentProperty> Properties { get; private set; }
    public EquipmentClass EquipmentClass { get; private set; }
    public HierarchyModel? HierarchyModel { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Equipment(): base() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Equipment(string equipmentId, string name, List<EquipmentProperty> properties, EquipmentClass equipmentClass, HierarchyModel? hierarchyModel): base(equipmentId)
    {
        Name = name;
        Properties = properties;
        EquipmentClass = equipmentClass;
        HierarchyModel = hierarchyModel;
    }

    public void Update(string name, List<EquipmentProperty> properties, EquipmentClass equipmentClass, HierarchyModel? hierarchyModel)
    {
        Name = name;
        Properties = properties;
        EquipmentClass = equipmentClass;
        HierarchyModel = hierarchyModel;
    }
}
