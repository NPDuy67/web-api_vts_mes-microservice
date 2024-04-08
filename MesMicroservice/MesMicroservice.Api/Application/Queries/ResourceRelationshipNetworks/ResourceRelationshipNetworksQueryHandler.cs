using MesMicroservice.Domain.AggregateModels.ResourceRelationshipNetworkAggregate;

namespace MesMicroservice.Api.Application.Queries.ResourceRelationshipNetworks;

public class ResourceRelationshipNetworksQueryHandler : IRequestHandler<ResourceRelationshipNetworksQuery, QueryResult<ResourceRelationshipNetworkViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ResourceRelationshipNetworksQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<ResourceRelationshipNetworkViewModel>> Handle(ResourceRelationshipNetworksQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ResourceRelationshipNetworks
            .Include(x => x.Connections)
            .ThenInclude(x => x.Properties)
            .Include(x => x.Connections)
            .ThenInclude(x => x.FromResource)
            .Include(x => x.Connections)
            .ThenInclude(x => x.ToResource)
            .AsNoTracking();

        if (request.IdStartedWith is not null)
        {
            queryable = queryable.Where(x => x.ResourceRelationshipNetworkId.StartsWith(request.IdStartedWith));
        }

        int totalItems = await queryable.CountAsync();

        if (request.Paginated)
        {
            queryable = queryable
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var relationships = await queryable.ToListAsync();
        var queryResult = new QueryResult<ResourceRelationshipNetwork>(relationships, totalItems);

        return _mapper.Map<QueryResult<ResourceRelationshipNetwork>, QueryResult<ResourceRelationshipNetworkViewModel>>(queryResult);
    }
}
