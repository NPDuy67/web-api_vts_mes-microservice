using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;

namespace MesMicroservice.Api.Application.Queries.Enterprises;

public class WorkUnitsQueryHandler : IRequestHandler<WorkUnitsQuery, QueryResult<WorkUnitViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public WorkUnitsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<WorkUnitViewModel>> Handle(WorkUnitsQuery request, CancellationToken cancellationToken)
    {
        var enterprises = await _context.Enterprises
                .Include(x => x.Sites)
                .ThenInclude(x => x.Areas)
                .ThenInclude(x => x.WorkCenters)
                .ThenInclude(x => x.WorkUnits)
                .ToListAsync();

        var workUnits = enterprises.SelectMany(x => x.Sites)
                .SelectMany(x => x.Areas)
                .SelectMany(x => x.WorkCenters)
                .SelectMany(x => x.WorkUnits);

        if (request.IdStartedWith is not null)
        {
            workUnits = workUnits.Where(x => x.HierarchyModelId.StartsWith(request.IdStartedWith));
        }

        int totalItems = workUnits.Count();

        workUnits = workUnits
                .OrderBy(x => x.HierarchyModelId)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);

        var queryResult = new QueryResult<WorkUnit>(workUnits, totalItems);
        return _mapper.Map<QueryResult<WorkUnit>, QueryResult<WorkUnitViewModel>>(queryResult);
    }
}
