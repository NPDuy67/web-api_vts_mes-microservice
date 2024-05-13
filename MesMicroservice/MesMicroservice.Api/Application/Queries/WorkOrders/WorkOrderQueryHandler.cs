using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Api.Application.Queries.WorkOrders;

public class WorkOrderQueryHandler : IRequestHandler<WorkOrderQuery, WorkOrderViewModel?>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public WorkOrderQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WorkOrderViewModel?> Handle(WorkOrderQuery request, CancellationToken cancellationToken)
    {
        var workOrder = await _context.WorkOrders
            .Include(x => x.ManufacturingRecords)
            .Include(x => x.ManufacturingOrder)
            .ThenInclude(x => x.MaterialDefinition)
            .Include(x => x.PrerequisiteOperations)
            .Include(x => x.WorkCenter)
            .ThenInclude(x => x!.Parent)
            .ThenInclude(x => x!.Parent)
            .ThenInclude(x => x!.Parent)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => 
                x.ManufacturingOrder.ManufacturingOrderId == request.ManufacturingOrderId &&
                x.WorkOrderId == request.WorkOrderId);

        return _mapper.Map<WorkOrder?, WorkOrderViewModel?>(workOrder);
    }
}
