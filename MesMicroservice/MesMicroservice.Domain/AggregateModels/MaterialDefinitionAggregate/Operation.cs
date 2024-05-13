namespace MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;
public class Operation : Entity
{
    public int MaterialDefinitionId { get; set; }
    public string OperationId { get; private set; }
    public string Name { get; private set; }
    public TimeSpan Duration { get; private set; }
    public List<Operation> PrerequisiteOperation { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Operation() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Operation(string operationId, string name, TimeSpan duration, List<Operation> prerequisiteOperation, int materialDefinitionId)
    {
        OperationId = operationId;
        Name = name;
        Duration = duration;
        PrerequisiteOperation = prerequisiteOperation;
        MaterialDefinitionId = materialDefinitionId;
    }

    public void Update(string name, TimeSpan duration, List<Operation> prerequisiteOperation)
    {
        Name = name;
        Duration = duration;
        PrerequisiteOperation = prerequisiteOperation;
    }
}
