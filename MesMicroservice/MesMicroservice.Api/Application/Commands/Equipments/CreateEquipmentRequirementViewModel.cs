namespace MesMicroservice.Api.Application.Commands.Equipments;

public class CreateEquipmentRequirementViewModel
{
    public string EquipmentClassId { get; set; }
    public int Quantity { get; set; }

    public CreateEquipmentRequirementViewModel(string equipmentClassId, int quantity)
    {
        EquipmentClassId = equipmentClassId;
        Quantity = quantity;
    }
}
