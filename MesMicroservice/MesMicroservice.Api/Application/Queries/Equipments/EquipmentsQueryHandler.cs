using MesMicroservice.Domain.AggregateModels.EquipmentAggregate;
using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;
using Microsoft.IdentityModel.Tokens;

namespace MesMicroservice.Api.Application.Queries.Equipments;

public class EquipmentsQueryHandler : IRequestHandler<EquipmentsQuery, QueryResult<EquipmentViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EquipmentsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<EquipmentViewModel>> Handle(EquipmentsQuery request, CancellationToken cancellationToken)
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

        if (request.IdStartedWith is not null)
        {
            queryable = queryable.Where(x => x.ResourceId.StartsWith(request.IdStartedWith));
        }

        int totalItems = await queryable.CountAsync(cancellationToken: cancellationToken);

        if (request.Paginated)
        {
            queryable = queryable
                .OrderBy(x => x.ResourceId)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var equipments = await queryable.ToListAsync(cancellationToken: cancellationToken);
        var queryResult = new QueryResult<Equipment>(equipments, totalItems);

        return _mapper.Map<QueryResult<Equipment>, QueryResult<EquipmentViewModel>>(queryResult);
    }
}
