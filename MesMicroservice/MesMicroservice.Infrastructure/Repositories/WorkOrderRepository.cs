using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Infrastructure.Repositories;

public class WorkOrderRepository : BaseRepository, IWorkOrderRepository
{
    public WorkOrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<WorkOrder> Add(int manufacturingOrderId, WorkOrder workOrder)
    {
        if (workOrder.IsTransient())
        {
            if (await ExistsAsync(manufacturingOrderId, workOrder.WorkOrderId))
            {
                throw new EntityDuplicationException(nameof(WorkOrder), workOrder.WorkOrderId);
            }

            return _context.WorkOrders
                .Add(workOrder)
                .Entity;
        }
        else
        {
            return workOrder;
        }
    }

    public async Task<WorkOrder?> GetAsync(int manufacturingOrderId, string workOrderId)
    {
        return await _context.WorkOrders
            .Include(x => x.ManufacturingOrder)
            .Include(x => x.PrerequisiteOperations)
            .Include(x => x.WorkCenter)
            .Include(x => x.ManufacturingRecords)
            .FirstOrDefaultAsync(x => x.ManufacturingOrderId == manufacturingOrderId && x.WorkOrderId == workOrderId);
    }

    public async Task<List<WorkOrder>> GetListByIdAsync(int manufacturingOrderId, List<string> workOrderIds)
    {
        var workOrders = await _context.WorkOrders
            .Include(x => x.ManufacturingOrder)
            .Include(x => x.PrerequisiteOperations)
            .Include(x => x.WorkCenter)
            .Include(x => x.ManufacturingRecords)
            .Where(x => x.ManufacturingOrderId == manufacturingOrderId && workOrderIds.Contains(x.WorkOrderId))
            .ToListAsync();

        var notFoundIds = workOrderIds
            .Where(id => !workOrders.Exists(pc => pc.WorkOrderId == id));

        if (notFoundIds.Any())
        {
            throw new EntitiesNotFoundException(nameof(WorkOrder), notFoundIds.ToList());
        }

        return workOrders;
    }

    public WorkOrder Update(WorkOrder workOrder)
    {
        return _context.WorkOrders
                .Update(workOrder)
                .Entity;
    }

    public async Task<bool> ExistsAsync(int manufacturingOrderId, string workOrderId)
    {
        return await _context.WorkOrders
            .AnyAsync(x => x.ManufacturingOrderId == manufacturingOrderId && x.WorkOrderId == workOrderId);
    }

    public async Task DeleteAsync(int manufacturingOrderId, string workOrderId)
    {
        var workOrder = await _context.WorkOrders
            .FirstOrDefaultAsync(x => x.ManufacturingOrderId == manufacturingOrderId && x.WorkOrderId == workOrderId);

        if (workOrder is not null)
        {
            _context.WorkOrders.Remove(workOrder);
        }
    }
}
