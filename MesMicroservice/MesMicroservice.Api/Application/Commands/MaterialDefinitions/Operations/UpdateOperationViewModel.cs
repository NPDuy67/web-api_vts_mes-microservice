namespace MesMicroservice.Api.Application.Commands.MaterialDefinitions.Operations;

[DataContract]
public class UpdateOperationViewModel
{
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public TimeSpan Duration { get; set; }
    [DataMember]
    public List<string> PrerequisiteOperation { get; set; }

    public UpdateOperationViewModel(string name, TimeSpan duration, List<string> prerequisiteOperation)
    {
        Name = name;
        Duration = duration;
        PrerequisiteOperation = prerequisiteOperation;
    }
}


