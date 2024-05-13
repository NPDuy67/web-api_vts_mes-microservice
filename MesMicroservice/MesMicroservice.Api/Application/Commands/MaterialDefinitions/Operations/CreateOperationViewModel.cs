namespace MesMicroservice.Api.Application.Commands.MaterialDefinitions.Operations;

[DataContract]
public class CreateOperationViewModel
{
    [DataMember]
    public string OperationId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public TimeSpan Duration { get; set; }
    [DataMember]
    public List<string> PrerequisiteOperation { get; set; }

    public CreateOperationViewModel(string operationId, string name, TimeSpan duration, List<string> prerequisiteOperation)
    {
        OperationId = operationId;
        Name = name;
        Duration = duration;
        PrerequisiteOperation = prerequisiteOperation;
    }
}
