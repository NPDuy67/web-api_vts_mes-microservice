using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;

namespace MesMicroservice.Api.Application.Queries.Enterprises;

public class WorkCenterViewModel
{
    public string WorkCenterId { get; set; }
    public string Name { get; set; }
    public string AbsolutePath { get; set; }
    public EWorkCenterType WorkCenterType { get; private set; }
    public List<WorkUnitViewModel> WorkUnits { get; set; }

    public WorkCenterViewModel(string workCenterId, string name, string absolutePath, EWorkCenterType workCenterType, List<WorkUnitViewModel> workUnits)
    {
        WorkCenterId = workCenterId;
        Name = name;
        AbsolutePath = absolutePath;
        WorkCenterType = workCenterType;
        WorkUnits = workUnits;
    }
}
