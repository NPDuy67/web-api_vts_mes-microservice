namespace MesMicroservice.Api.Application.Commands.MaterialDefinitions.Operations;

public class CreateOperationCommand : IRequest<bool>
{
    public string MaterialDefinitionId { get; set; }
    public string OperationId { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public List<string> PrerequisiteOperation { get; set; }

    public CreateOperationCommand(string materialDefinitionId, string operationId, string name, TimeSpan duration, List<string> prerequisiteOperation)
    {
        MaterialDefinitionId = materialDefinitionId;
        OperationId = operationId;
        Name = name;
        Duration = duration;
        PrerequisiteOperation = prerequisiteOperation;
    }
}
