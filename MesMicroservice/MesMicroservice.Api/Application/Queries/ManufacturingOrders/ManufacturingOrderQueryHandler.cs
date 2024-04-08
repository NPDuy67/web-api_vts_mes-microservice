using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;

namespace MesMicroservice.Api.Application.Queries.ManufacturingOrders;

public class ManufacturingOrderQueryHandler : IRequestHandler<ManufacturingOrderQuery, ManufacturingOrderViewModel?>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ManufacturingOrderQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ManufacturingOrderViewModel?> Handle(ManufacturingOrderQuery request, CancellationToken cancellationToken)
    {
        var manufacturingOrder = await _context
            .ManufacturingOrders
            .Include(x => x.MaterialDefinition)
            .ThenInclude(x => x.Operations)
            .Include(x => x.MaterialDefinition)
            .ThenInclude(x => x.MaterialClass)
            .Include(x => x.WorkOrders)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ManufacturingOrderId == request.ManufacturingOrderId);

        return _mapper.Map<ManufacturingOrder?, ManufacturingOrderViewModel?>(manufacturingOrder);
    }
}
