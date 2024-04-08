namespace MesMicroservice.Api.Application.Queries.Enterprises;

public class EnterprisesQuery : PaginatedQuery, IRequest<QueryResult<EnterpriseViewModel>>
{
    public string? IdStartedWith { get; set; }
}
