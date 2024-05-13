using MesMicroservice.Domain.AggregateModels.MaterialClassAggregate;

namespace MesMicroservice.Api.Application.Queries.MaterialClasses;

public class MaterialClassesQueryHandler : IRequestHandler<MaterialClassesQuery, QueryResult<MaterialClassViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MaterialClassesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<MaterialClassViewModel>> Handle(MaterialClassesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.MaterialClasses
            .Include(x => x.MaterialDefinitions)
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

        var materialClasses = await queryable.ToListAsync();
        var queryResult = new QueryResult<MaterialClass>(materialClasses, totalItems);

        return _mapper.Map<QueryResult<MaterialClass>, QueryResult<MaterialClassViewModel>>(queryResult);
    }
}
