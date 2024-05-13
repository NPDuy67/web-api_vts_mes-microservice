namespace MesMicroservice.Api.Application.Queries.EquipmentClasses;

public class EquipmentClassesQuery : PaginatedQuery, IRequest<QueryResult<EquipmentClassViewModel>>
{
    public string? IdStartedWith { get; set; }
}
