using MesMicroservice.Api.Application.Commands.Equipments;

namespace MesMicroservice.Api.Application.Commands.MaterialDefinitions.Operations;

public class CreateOperationCommand : IRequest<bool>
{
    public string MaterialDefinitionId { get; set; }
    public string OperationId { get; set; }
    public string Name { get; set; }
    public List<string> PrerequisiteOperation { get; set; }
    public List<CreateEquipmentRequirementViewModel> EquipmentRequirements { get; set; }

    public CreateOperationCommand(string materialDefinitionId, string operationId, string name, List<string> prerequisiteOperation, List<CreateEquipmentRequirementViewModel> equipmentRequirements)
    {
        MaterialDefinitionId = materialDefinitionId;
        OperationId = operationId;
        Name = name;
        PrerequisiteOperation = prerequisiteOperation;
        EquipmentRequirements = equipmentRequirements;
    }
}
