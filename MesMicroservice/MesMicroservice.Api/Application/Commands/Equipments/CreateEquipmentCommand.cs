namespace MesMicroservice.Api.Application.Commands.Equipments;

[DataContract]
public class CreateEquipmentCommand: IRequest<bool>
{
    [DataMember]
    public string EquipmentId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public List<SavePropertyViewModel> Properties { get; set; }
    [DataMember]
    public string EquipmentClass { get; set; }
    [DataMember]
    public string? AbsolutePath { get; set; }

    public CreateEquipmentCommand(string equipmentId, string name, List<SavePropertyViewModel> properties, string equipmentClass, string? absolutePath)
    {
        EquipmentId = equipmentId;
        Name = name;
        Properties = properties;
        EquipmentClass = equipmentClass;
        AbsolutePath = absolutePath;
    }
}
