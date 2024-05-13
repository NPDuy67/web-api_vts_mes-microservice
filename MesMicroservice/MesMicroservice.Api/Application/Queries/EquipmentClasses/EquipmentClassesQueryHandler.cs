using MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;

namespace MesMicroservice.Api.Application.Queries.EquipmentClasses;

public class EquipmentClassesQueryHandler: IRequestHandler<EquipmentClassesQuery, QueryResult<EquipmentClassViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EquipmentClassesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<EquipmentClassViewModel>> Handle(EquipmentClassesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EquipmentClasses
            .Include(x => x.Equipments)
            .ThenInclude(x => x.HierarchyModel)
            .AsNoTracking();

        if (request.IdStartedWith is not null)
        {
            queryable = queryable.Where(x => x.ResourceId.StartsWith(request.IdStartedWith));
        }

        int totalItems = await queryable.CountAsync();

        if (request.Paginated)
        {
            queryable = queryable
                .OrderBy(x => x.ResourceId)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var equipmentClasses = await queryable.ToListAsync();
        var queryResult = new QueryResult<EquipmentClass>(equipmentClasses, totalItems);

        return _mapper.Map<QueryResult<EquipmentClass>, QueryResult<EquipmentClassViewModel>>(queryResult);
    }
}
