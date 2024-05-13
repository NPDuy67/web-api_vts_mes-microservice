namespace MesMicroservice.Api.Application.Queries.Equipments;

public class EquipmentQuery: IRequest<EquipmentViewModel?>
{
    public string EquipmentId { get; set; }

    public EquipmentQuery(string equipmentId)
    {
        EquipmentId = equipmentId;
    }
}
