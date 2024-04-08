using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;

namespace MesMicroservice.Api.Application.Commands.Enterprises.WorkCenters;

public class CreateWorkCenterCommand : IRequest<bool>
{
    public string EnterpriseId { get; set; }
    public string SiteId { get; set; }
    public string AreaId { get; set; }
    public string WorkCenterId { get; set; }
    public string Name { get; set; }
    public EWorkCenterType WorkCenterType { get; set; }

    public CreateWorkCenterCommand(string enterpriseId, string siteId, string areaId, string workCenterId, string name, EWorkCenterType workCenterType)
    {
        EnterpriseId = enterpriseId;
        SiteId = siteId;
        AreaId = areaId;
        WorkCenterId = workCenterId;
        Name = name;
        WorkCenterType = workCenterType;
    }
}
