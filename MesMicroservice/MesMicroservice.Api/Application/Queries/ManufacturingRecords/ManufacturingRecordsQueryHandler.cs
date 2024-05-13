using MesMicroservice.Domain.AggregateModels.ManufacucturingRecordAggregate;

namespace MesMicroservice.Api.Application.Queries.ManufacturingRecords;

public class ManufacturingRecordsQueryHandler : IRequestHandler<ManufacturingRecordsQuery, QueryResult<ManufacturingRecordViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ManufacturingRecordsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QueryResult<ManufacturingRecordViewModel>> Handle(ManufacturingRecordsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ManufacturingRecords
            .Include(x => x.WorkOrder)
            .Include(x => x.Equipments)
            .Include(x => x.OutputMaterialDefinition)
            .AsNoTracking();

        if (request.StartTime is not null)
        {
            queryable = queryable.Where(x => x.StartTime > request.StartTime);
        }

        if (request.EndTime is not null)
        {
            queryable = queryable.Where(x => x.EndTime < request.EndTime);
        }

        if (request.EquipmentId is not null)
        {
            queryable = queryable.Where(x => x.Equipments.Any(x => x.ResourceId == request.EquipmentId));
        }

        int totalItems = await queryable.CountAsync();

        if (request.Paginated)
        {
            queryable = queryable
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var manufacturingRecords = await queryable.ToListAsync();
        var queryResult = new QueryResult<ManufacturingRecord>(manufacturingRecords, totalItems);

        return _mapper.Map<QueryResult<ManufacturingRecord>, QueryResult<ManufacturingRecordViewModel>>(queryResult);
    }
}
