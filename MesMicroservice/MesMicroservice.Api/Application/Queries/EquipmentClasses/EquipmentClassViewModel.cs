namespace MesMicroservice.Api.Application.Queries.EquipmentClasses;

public class EquipmentClassViewModel
{
    public string EquipmentClassId { get; set; }
    public string Name { get; set; }
    public List<string> Equipments { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EquipmentClassViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public EquipmentClassViewModel(string equipmentClassId, string name, List<string> equipments)
    {
        EquipmentClassId = equipmentClassId;
        Name = name;
        Equipments = equipments;
    }
}
