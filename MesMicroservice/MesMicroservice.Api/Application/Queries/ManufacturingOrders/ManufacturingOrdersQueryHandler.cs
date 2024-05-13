using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace MesMicroservice.Api.Application.Queries.ManufacturingOrders;

public class ManufacturingOrdersQueryHandler : IRequestHandler<ManufacturingOrdersQuery, QueryResult<ManufacturingOrderViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ManufacturingOrdersQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<ManufacturingOrderViewModel>> Handle(ManufacturingOrdersQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ManufacturingOrders
            .Include(x => x.MaterialDefinition)
            .ThenInclude(x => x.Operations)
            .Include(x => x.MaterialDefinition)
            .ThenInclude(x => x.MaterialClass)
            .Include(x => x.WorkOrders)
            .AsNoTracking();

        if (request.IdStartedWith is not null)
        {
            queryable = queryable.Where(x => x.ManufacturingOrderId.StartsWith(request.IdStartedWith));
        }

        int totalItems = await queryable.CountAsync();

        if (request.Paginated)
        {
            queryable = queryable
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var manufacturingOrders = await queryable.ToListAsync();
        var queryResult = new QueryResult<ManufacturingOrder>(manufacturingOrders, totalItems);

        return _mapper.Map<QueryResult<ManufacturingOrder>, QueryResult<ManufacturingOrderViewModel>>(queryResult);
    }
}
