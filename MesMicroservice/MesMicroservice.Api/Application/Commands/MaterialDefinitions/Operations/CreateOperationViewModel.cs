using MesMicroservice.Api.Application.Commands.Equipments;

namespace MesMicroservice.Api.Application.Commands.MaterialDefinitions.Operations;

[DataContract]
public class CreateOperationViewModel
{
    [DataMember]
    public string OperationId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public List<string> PrerequisiteOperation { get; set; }
    [DataMember]
    public List<CreateEquipmentRequirementViewModel> EquipmentRequirements { get; set; }

    public CreateOperationViewModel(string operationId, string name, List<string> prerequisiteOperation, List<CreateEquipmentRequirementViewModel> equipmentRequirements)
    {
        OperationId = operationId;
        Name = name;
        PrerequisiteOperation = prerequisiteOperation;
        EquipmentRequirements = equipmentRequirements;
    }
}
