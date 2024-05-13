using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;

namespace MesMicroservice.Api.Application.Commands.Enterprises.WorkUnits;
[DataContract]
public class UpdateWorkUnitViewModel
{
    [DataMember]
    public string WorkUnitId { get; set; }
    [DataMember]
    public string Name { get; set; }

    public UpdateWorkUnitViewModel(string workUnitId, string name)
    {
        WorkUnitId = workUnitId;
        Name = name;
    }
}
