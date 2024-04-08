namespace MesMicroservice.Api.Application.Queries.MaterialLots;

public class MaterialLotsQuery : PaginatedQuery, IRequest<QueryResult<MaterialLotViewModel>>
{
    public string? IdStartedWith { get; set; }
    public string? MaterialDefinitionId { get; set; }
}
