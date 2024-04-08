namespace MesMicroservice.Api.Application.Commands.MaterialClasses;

[DataContract]
public class CreateMaterialClassCommand : IRequest<bool>
{
    [DataMember]
    public string MaterialClassId { get; set; }
    [DataMember]
    public string Name { get; set; }

    public CreateMaterialClassCommand(string materialClassId, string name)
    {
        MaterialClassId = materialClassId;
        Name = name;
    }
}
