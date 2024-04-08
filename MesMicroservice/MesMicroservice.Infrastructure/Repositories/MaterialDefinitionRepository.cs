using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;

namespace MesMicroservice.Infrastructure.Repositories;
public class MaterialDefinitionRepository : BaseRepository, IMaterialDefinitionRepository
{
    public MaterialDefinitionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<MaterialDefinition> Add(MaterialDefinition materialDefinition)
    {
        if (materialDefinition.IsTransient())
        {
            if (await ExistsAsync(materialDefinition.ResourceId))
            {
                throw new EntityDuplicationException(nameof(MaterialDefinition), materialDefinition.ResourceId);
            }

            return _context.MaterialDefinitions
                .Add(materialDefinition)
                .Entity;
        }
        else
        {
            return materialDefinition;
        }
    }

    public async Task<MaterialDefinition?> GetAsync(string materialDefinitionId)
    {
        return await _context.MaterialDefinitions
            .Include(x => x.MaterialLots)
            .Include(x => x.SecondaryUnits)
            .Include(x => x.Operations)
            .ThenInclude(x => x.PrerequisiteOperation)
            .FirstOrDefaultAsync(x => x.ResourceId == materialDefinitionId);
    }

    public async Task<List<MaterialDefinition>> GetListByIdAsync(List<string> materialDefinitionIds)
    {
        var materialDefinitions = await _context.MaterialDefinitions
            .Include(x => x.MaterialLots)
            .Include(x => x.SecondaryUnits)
            .Include(x => x.Operations)
            .Where(x => materialDefinitionIds.Contains(x.ResourceId))
            .ToListAsync();

        var notFoundIds = materialDefinitionIds
            .Where(id => !materialDefinitions.Exists(pc => pc.ResourceId == id));

        if (notFoundIds.Any())
        {
            throw new EntitiesNotFoundException(nameof(MaterialDefinition), notFoundIds.ToList());
        }

        return materialDefinitions;
    }

    public MaterialDefinition Update(MaterialDefinition materialDefinition)
    {
        return _context.MaterialDefinitions
                .Update(materialDefinition)
                .Entity;
    }

    public async Task<bool> ExistsAsync(string materialDefinitionId)
    {
        return await _context.MaterialDefinitions
            .AnyAsync(x => x.ResourceId == materialDefinitionId);
    }

    public async Task DeleteAsync(string materialDefinitionId)
    {
        var materialDefinition = await _context.MaterialDefinitions
            .FirstOrDefaultAsync(x => x.ResourceId == materialDefinitionId);

        if (materialDefinition is not null)
        {
            _context.MaterialDefinitions.Remove(materialDefinition);
        }
    }

    public async Task DeleteMaterialUnitAsync(string unitId)
    {
        var materialUnit = await _context.MaterialDefinitions
            .SelectMany(x => x.SecondaryUnits)
            .FirstOrDefaultAsync(x => x.UnitId == unitId);

        if (materialUnit is not null)
        {
            _context.Set<MaterialUnit>().Remove(materialUnit);
        }
    }

    public async Task DeleteOperationAsync(string operationId)
    {
        var operation = await _context.MaterialDefinitions
            .SelectMany(x => x.Operations)
            .FirstOrDefaultAsync(x => x.OperationId == operationId);

        if (operation is not null)
        {
            _context.Set<Operation>().Remove(operation);
        }
    }
}
