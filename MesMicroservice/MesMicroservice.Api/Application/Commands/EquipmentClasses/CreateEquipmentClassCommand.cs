namespace MesMicroservice.Api.Application.Commands.EquipmentClasses;

[DataContract]
public class CreateEquipmentClassCommand : IRequest<bool>
{
    [DataMember]
    public string EquipmentClassId { get; set; }
    [DataMember]
    public string Name { get; set; }

    public CreateEquipmentClassCommand(string equipmentClassId, string name)
    {
        EquipmentClassId = equipmentClassId;
        Name = name;
    }
}
