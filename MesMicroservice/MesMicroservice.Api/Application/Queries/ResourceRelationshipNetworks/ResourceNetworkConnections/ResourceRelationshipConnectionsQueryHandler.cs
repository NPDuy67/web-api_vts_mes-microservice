using MesMicroservice.Domain.AggregateModels.ResourceRelationshipNetworkAggregate;

namespace MesMicroservice.Api.Application.Queries.ResourceRelationshipNetworks.ResourceNetworkConnections;

public class ResourceRelationshipConnectionsQueryHandler : IRequestHandler<ResourceRelationshipConnectionsQuery, QueryResult<ResourceNetworkConnectionViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ResourceRelationshipConnectionsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<ResourceNetworkConnectionViewModel>> Handle(ResourceRelationshipConnectionsQuery request, CancellationToken cancellationToken)
    {
        var networks = _context.ResourceRelationshipNetworks
            .Where(x => x.ResourceRelationshipNetworkId == request.NetworkId);

        var queryable = networks
            .SelectMany(x => x.Connections)
            .Include(x => x.FromResource)
            .Include(x => x.ToResource)
            .AsNoTracking();

        if (request.IdStartedWith is not null)
        {
            queryable = queryable.Where(x => x.ConnectionId.StartsWith(request.IdStartedWith));
        }

        if (request.FromResourceId is not null)
        {
            queryable = queryable.Where(x => x.FromResource.ResourceId == request.FromResourceId);
        }

        if (request.ToResourceId is not null)
        {
            queryable = queryable.Where(x => x.ToResource.ResourceId == request.ToResourceId);
        }

        int totalItems = await queryable.CountAsync(cancellationToken);

        if (request.Paginated)
        {
            queryable = queryable
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var connections = await queryable.ToListAsync(cancellationToken);
        var queryResult = new QueryResult<ResourceNetworkConnection>(connections, totalItems);

        return _mapper.Map<QueryResult<ResourceNetworkConnection>, QueryResult<ResourceNetworkConnectionViewModel>>(queryResult);
    }
}
