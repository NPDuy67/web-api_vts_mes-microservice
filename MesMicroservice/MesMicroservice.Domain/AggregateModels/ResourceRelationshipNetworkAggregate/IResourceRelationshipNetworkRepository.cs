namespace MesMicroservice.Domain.AggregateModels.ResourceRelationshipNetworkAggregate;

public interface IResourceRelationshipNetworkRepository : IRepository<ResourceRelationshipNetwork>
{
    public Task<ResourceRelationshipNetwork> Add(ResourceRelationshipNetwork resourceRelationshipNetwork);
    public Task<ResourceRelationshipNetwork?> GetAsync(string resourceRelationshipNetworkId);
    public ResourceRelationshipNetwork Update(ResourceRelationshipNetwork resourceRelationshipNetwork);
    public Task<bool> ExistsAsync(string resourceRelationshipNetworkId);
    public Task DeleteAsync(string resourceRelationshipNetworkId);
}
