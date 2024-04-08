namespace MesMicroservice.Api.Application.Queries.WorkCenterOutputs;

public class WorkCenterOutputsByManuOrderQuery : IRequest<IEnumerable<WorkCenterOutputViewModel>>
{
    public string ManufacturingOrderId { get; set; }

    public WorkCenterOutputsByManuOrderQuery(string manufacturingOrderId)
    {
        ManufacturingOrderId = manufacturingOrderId;
    }
}
