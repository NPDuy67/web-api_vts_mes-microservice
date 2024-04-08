namespace MesMicroservice.Api.Application.Queries.ResourceRelationshipNetworks;

public class ResourceRelationshipNetworksQuery : PaginatedQuery, IRequest<QueryResult<ResourceRelationshipNetworkViewModel>>
{
    public string? IdStartedWith { get; set; }
}
