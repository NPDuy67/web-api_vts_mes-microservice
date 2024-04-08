namespace MesMicroservice.Api.Application.Commands.MaterialDefinitions;

[DataContract]
public class CreateMaterialDefinitionCommand: IRequest<bool>
{
    [DataMember]
    public string MaterialDefinitionId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string PrimaryUnit { get; set; }
    [DataMember]
    public List<SavePropertyViewModel> Properties { get; set; }
    [DataMember]
    public string MaterialClass { get; set; }

    public CreateMaterialDefinitionCommand(string materialDefinitionId, string name, string primaryUnit, List<SavePropertyViewModel> properties, string materialClass)
    {
        MaterialDefinitionId = materialDefinitionId;
        Name = name;
        PrimaryUnit = primaryUnit;
        Properties = properties;
        MaterialClass = materialClass;
    }
}
