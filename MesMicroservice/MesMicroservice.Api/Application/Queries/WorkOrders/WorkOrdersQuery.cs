using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Api.Application.Queries.WorkOrders;

public class WorkOrdersQuery : PaginatedQuery, IRequest<QueryResult<WorkOrderViewModel>>
{
    public string? ManufacturingOrderId { get; set; }
    public string? IdStartedWith { get; set; }
    public EWorkOrderStatus? Status { get; set; }
    public string? WorkCenterId { get; set; }
}
