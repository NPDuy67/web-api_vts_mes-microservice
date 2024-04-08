using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;
using System.Linq;

namespace MesMicroservice.Api.Application.Queries.Enterprises;

public class EnterprisesQueryHandler : IRequestHandler<EnterprisesQuery, QueryResult<EnterpriseViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EnterprisesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<EnterpriseViewModel>> Handle(EnterprisesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Enterprises
            .Include(x => x.Sites)
            .ThenInclude(x => x.Areas)
            .ThenInclude(x => x.WorkCenters)
            .ThenInclude(x => x.WorkUnits)
            .AsNoTracking();

        if (request.IdStartedWith is not null)
        {
            queryable = queryable.Where(x => x.HierarchyModelId.StartsWith(request.IdStartedWith));
        }

        int totalItems = await queryable.CountAsync();

        queryable = queryable
            .OrderBy(x => x.HierarchyModelId)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize);

        var enterprises = await queryable.ToListAsync();
        var queryResult = new QueryResult<Enterprise>(enterprises, totalItems);

        return _mapper.Map<QueryResult<Enterprise>, QueryResult<EnterpriseViewModel>>(queryResult);
    }
}
