using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;

namespace MesMicroservice.Api.Application.Queries.MaterialDefinitions;

public class MaterialDefinitionsQueryHandler : IRequestHandler<MaterialDefinitionsQuery, QueryResult<MaterialDefinitionViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MaterialDefinitionsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<MaterialDefinitionViewModel>> Handle(MaterialDefinitionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.MaterialDefinitions
            .Include(x => x.Properties)
            .Include(x => x.SecondaryUnits)
            .Include(x => x.Operations)
            .ThenInclude(x => x.PrerequisiteOperation)
            .Include(x => x.MaterialClass)
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

        var materialDefintions = await queryable.ToListAsync();
        var queryResult = new QueryResult<MaterialDefinition>(materialDefintions, totalItems);

        return _mapper.Map<QueryResult<MaterialDefinition>, QueryResult<MaterialDefinitionViewModel>>(queryResult);
    }
}
