namespace MesMicroservice.Api.Application.Queries.MaterialClasses;

public class MaterialClassesQuery : PaginatedQuery, IRequest<QueryResult<MaterialClassViewModel>>
{
    public string? IdStartedWith { get; set; }
}
