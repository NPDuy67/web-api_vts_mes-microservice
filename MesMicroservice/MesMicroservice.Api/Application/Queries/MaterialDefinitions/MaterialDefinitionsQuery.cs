namespace MesMicroservice.Api.Application.Queries.MaterialDefinitions;

public class MaterialDefinitionsQuery : PaginatedQuery, IRequest<QueryResult<MaterialDefinitionViewModel>>
{
    public string? IdStartedWith { get; set; }
}
