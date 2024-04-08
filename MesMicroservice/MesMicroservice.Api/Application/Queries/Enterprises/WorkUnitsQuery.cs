namespace MesMicroservice.Api.Application.Queries.Enterprises;
public class WorkUnitsQuery : PaginatedQuery, IRequest<QueryResult<WorkUnitViewModel>>
{
    public string? IdStartedWith { get; set; }
}
