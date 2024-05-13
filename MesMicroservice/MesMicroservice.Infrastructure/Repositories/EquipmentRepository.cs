using MesMicroservice.Domain.AggregateModels.EquipmentAggregate;
using MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;
using MesMicroservice.Infrastructure.Exceptions;

namespace MesMicroservice.Infrastructure.Repositories;
public class EquipmentRepository : BaseRepository, IEquipmentRepository
{
    public EquipmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Equipment> Add(Equipment equipment)
    {
        if (equipment.IsTransient())
        {
            if (await ExistsAsync(equipment.ResourceId))
            {
                throw new EntityDuplicationException($"Equipment with id {equipment.Id} already exists.");
            }

            return _context.Equipments
                .Add(equipment)
                .Entity;
        }
        else
        {
            return equipment;
        }
    }

    public async Task<Equipment?> GetAsync(string equipmentId)
    {
        return await _context.Equipments
            .Include(x => x.EquipmentClass)
            .Include(x => x.HierarchyModel)
            .FirstOrDefaultAsync(x => x.ResourceId == equipmentId);
    }

    public async Task<List<Equipment>> GetListByIdAsync(List<string> equipmentIds)
    {
        var equipments = await _context.Equipments
            .Include(x => x.EquipmentClass)
            .Include(x => x.HierarchyModel)
            .Where(x => equipmentIds.Contains(x.ResourceId))
            .ToListAsync();

        var notFoundIds = equipmentIds
            .Where(id => !equipments.Exists(pc => pc.ResourceId == id));

        if (notFoundIds.Any())
        {
            throw new EntitiesNotFoundException(nameof(Equipment), notFoundIds.ToList());
        }

        return equipments;
    }

    public Equipment Update(Equipment equipment)
    {
        return _context.Equipments
                .Update(equipment)
                .Entity;
    }

    public async Task<bool> ExistsAsync(string equipmentId)
    {
        return await _context.Equipments
            .AnyAsync(x => x.ResourceId == equipmentId);
    }

    public async Task DeleteAsync(string equipmentId)
    {
        var equipment = await _context.Equipments
            .FirstOrDefaultAsync(x => x.ResourceId == equipmentId);

        if (equipment is not null)
        {
            _context.Equipments.Remove(equipment);
        }
    }
}
