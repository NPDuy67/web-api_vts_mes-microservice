using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Api.Application.Queries.WorkOrders;

public class WorkOrdersQueryHandler : IRequestHandler<WorkOrdersQuery, QueryResult<WorkOrderViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public WorkOrdersQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<WorkOrderViewModel>> Handle(WorkOrdersQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.WorkOrders
            .Include(x => x.ManufacturingRecords)
            .Include(x => x.ManufacturingOrder)
            .ThenInclude(x => x.MaterialDefinition)
            .Include(x => x.PrerequisiteOperations)
            .Include(x => x.WorkCenter)
            .ThenInclude(x => x!.Parent)
            .ThenInclude(x => x!.Parent)
            .ThenInclude(x => x!.Parent)
            .AsNoTracking();

        if (request.IdStartedWith is not null)
        {
            queryable = queryable.Where(x => x.WorkOrderId.StartsWith(request.IdStartedWith));
        }

        if (request.ManufacturingOrderId is not null)
        {
            queryable = queryable.Where(x => x.ManufacturingOrder.ManufacturingOrderId == request.ManufacturingOrderId);
        }

        if (request.Status is not null)
        {
            queryable = queryable.Where(x => x.WorkOrderStatus == request.Status);
        }

        if (request.WorkCenterId is not null)
        {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            queryable = queryable.Where(x => x.WorkCenter != null && x.WorkCenter.HierarchyModelId == request.WorkCenterId);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.
        }

        int totalItems = await queryable.CountAsync();

        queryable = queryable
            .OrderBy(x => x.WorkOrderId)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize);

        var workOrders = await queryable.ToListAsync();
        var queryResult = new QueryResult<WorkOrder>(workOrders, totalItems);

        return _mapper.Map<QueryResult<WorkOrder>, QueryResult<WorkOrderViewModel>>(queryResult);
    }
}
