namespace MesMicroservice.Api.Application.Queries.Equipments;

public class EquipmentViewModel
{
    public string EquipmentId { get; set; }
    public string Name { get; set; }
    public List<PropertyViewModel> Properties { get; set; }
    public string AbsolutePath { get; set; }
    public string EquipmentClass { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EquipmentViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public EquipmentViewModel(string equipmentId, string name, List<PropertyViewModel> properties, string absolutePath, string equipmentClass)
    {
        EquipmentId = equipmentId;
        Name = name;
        Properties = properties;
        AbsolutePath = absolutePath;
        EquipmentClass = equipmentClass;
    }
}
