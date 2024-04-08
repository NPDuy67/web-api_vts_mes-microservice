using MesMicroservice.Domain.AggregateModels.MaterialLotAggregate;

namespace MesMicroservice.Api.Application.Queries.MaterialLots;

public class MaterialLotsQueryHandler : IRequestHandler<MaterialLotsQuery, QueryResult<MaterialLotViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MaterialLotsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<MaterialLotViewModel>> Handle(MaterialLotsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.MaterialLots
            .Include(x => x.MaterialDefinition)
            .Include(x => x.Unit)
            .AsNoTracking();

        if (request.IdStartedWith is not null)
        {
            queryable = queryable.Where(x => x.ResourceId.StartsWith(request.IdStartedWith));
        }

        if (request.MaterialDefinitionId is not null)
        {
            queryable = queryable.Where(x => x.MaterialDefinition.ResourceId == request.MaterialDefinitionId);
        }

        int totalItems = await queryable.CountAsync();

        if (request.Paginated)
        {
            queryable = queryable
                .OrderBy(x => x.ResourceId)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var materialLots = await queryable.ToListAsync();
        var queryResult = new QueryResult<MaterialLot>(materialLots, totalItems);

        return _mapper.Map<QueryResult<MaterialLot>, QueryResult<MaterialLotViewModel>>(queryResult);
    }
}