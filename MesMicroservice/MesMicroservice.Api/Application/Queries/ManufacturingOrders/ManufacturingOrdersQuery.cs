namespace MesMicroservice.Api.Application.Queries.ManufacturingOrders;

public class ManufacturingOrdersQuery : PaginatedQuery, IRequest<QueryResult<ManufacturingOrderViewModel>>
{
    public string? IdStartedWith { get; set; }
}
