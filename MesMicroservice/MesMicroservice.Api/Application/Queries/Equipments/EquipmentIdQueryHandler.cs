namespace MesMicroservice.Api.Application.Queries.Equipments;

public class EquipmentIdQueryHandler : IRequestHandler<EquipmentIdQuery, IEnumerable<EquipmentIdViewModel>>
{
    private readonly ApplicationDbContext _context;

    public EquipmentIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EquipmentIdViewModel>> Handle(EquipmentIdQuery request, CancellationToken cancellationToken)
    {
        var equipments = await _context.Equipments.ToListAsync(cancellationToken);

        return equipments.Select(x => new EquipmentIdViewModel(x.ResourceId, x.Name));
    }
}
