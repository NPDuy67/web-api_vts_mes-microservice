using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;

namespace MesMicroservice.Infrastructure.Repositories;

public class ManufacturingOrderRepository : BaseRepository, IManufacturingOrderRepository
{
    public ManufacturingOrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ManufacturingOrder> Add(ManufacturingOrder manufacturingOrder)
    {
        if (manufacturingOrder.IsTransient())
        {
            if (await ExistsAsync(manufacturingOrder.ManufacturingOrderId))
            {
                throw new EntityDuplicationException(nameof(ManufacturingOrder), manufacturingOrder.ManufacturingOrderId);
            }

            return _context.ManufacturingOrders
                .Add(manufacturingOrder)
                .Entity;
        }
        else
        {
            return manufacturingOrder;
        }
    }

    public async Task<ManufacturingOrder?> GetAsync(string manufacturingOrderId)
    {
        return await _context.ManufacturingOrders
            .Include(x => x.MaterialDefinition)
            .ThenInclude(x => x.Operations)
            .Include(x => x.MaterialDefinition)
            .ThenInclude(x => x.MaterialClass)
            .Include(x => x.WorkOrders)
            .ThenInclude(x => x.PrerequisiteOperations)
            .Include(x => x.WorkOrders)
            .ThenInclude(x => x.ManufacturingRecords)
            .FirstOrDefaultAsync(x => x.ManufacturingOrderId == manufacturingOrderId);
    }

    public async Task<List<ManufacturingOrder>> GetListByIdAsync(List<string> manufacturingOrderIds)
    {
        var manufacturingOrders = await _context.ManufacturingOrders
            .Include(x => x.MaterialDefinition)
            .ThenInclude(x => x.Operations)
            .Include(x => x.MaterialDefinition)
            .ThenInclude(x => x.MaterialClass)
            .Include(x => x.WorkOrders)
            .Where(x => manufacturingOrderIds.Contains(x.ManufacturingOrderId))
            .ToListAsync();

        var notFoundIds = manufacturingOrderIds
            .Where(id => !manufacturingOrders.Exists(pc => pc.ManufacturingOrderId == id));

        if (notFoundIds.Any())
        {
            throw new EntitiesNotFoundException(nameof(ManufacturingOrder), notFoundIds.ToList());
        }

        return manufacturingOrders;
    }

    public ManufacturingOrder Update(ManufacturingOrder manufacturingOrder)
    {
        return _context.ManufacturingOrders
                .Update(manufacturingOrder)
                .Entity;
    }

    public async Task<bool> ExistsAsync(string manufacturingOrderId)
    {
        return await _context.ManufacturingOrders
            .AnyAsync(x => x.ManufacturingOrderId == manufacturingOrderId);
    }

    public async Task DeleteAsync(string manufacturingOrderId)
    {
        var manufacturingOrder = await _context.ManufacturingOrders
            .FirstOrDefaultAsync(x => x.ManufacturingOrderId == manufacturingOrderId);

        if (manufacturingOrder is not null)
        {
            _context.ManufacturingOrders.Remove(manufacturingOrder);
        }
    }
}
