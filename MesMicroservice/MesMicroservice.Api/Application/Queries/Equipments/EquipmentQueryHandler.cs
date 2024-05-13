using MesMicroservice.Domain.AggregateModels.EquipmentAggregate;
using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;

namespace MesMicroservice.Api.Application.Queries.Equipments;

public class EquipmentQueryHandler : IRequestHandler<EquipmentQuery, EquipmentViewModel?>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EquipmentQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EquipmentViewModel?> Handle(EquipmentQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Equipments
            .Include(x => x.Properties)
            .Include(x => x.EquipmentClass)

            .Include(x => x.HierarchyModel)
            .ThenInclude(x => (x as Site).Parent)

            .Include(x => x.HierarchyModel)
            .ThenInclude(x => (x as Area).Parent)
            .ThenInclude(x => (x as Site).Parent)

            .Include(x => x.HierarchyModel)
            .ThenInclude(x => (x as WorkCenter).Parent)
            .ThenInclude(x => (x as Area).Parent)
            .ThenInclude(x => (x as Site).Parent)

            .Include(x => x.HierarchyModel)
            .ThenInclude(x => (x as WorkUnit).Parent)
            .ThenInclude(x => (x as WorkCenter).Parent)
            .ThenInclude(x => (x as Area).Parent)
            .ThenInclude(x => (x as Site).Parent)

            .AsNoTracking();

        var equipment = await queryable.FirstOrDefaultAsync(x => x.ResourceId == request.EquipmentId, cancellationToken);

        return _mapper.Map<Equipment? ,EquipmentViewModel?>(equipment);
    }
}
