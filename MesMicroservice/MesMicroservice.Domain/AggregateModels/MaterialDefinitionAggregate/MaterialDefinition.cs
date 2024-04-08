using MesMicroservice.Domain.AggregateModels.MaterialClassAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialLotAggregate;

namespace MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;
public class MaterialDefinition : Resource, IAggregateRoot
{
    public string Name { get; private set; }
    public string PrimaryUnit { get; private set; }
    public List<MaterialDefinitionProperty> Properties { get; private set; }
    public MaterialClass MaterialClass { get; private set; }
    public List<MaterialLot> MaterialLots { get; private set; }
    public List<MaterialUnit> SecondaryUnits { get; private set; }
    public List<Operation> Operations { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private MaterialDefinition() : base() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public MaterialDefinition(string materialDefinitionId, string name, string primaryUnit, List<MaterialDefinitionProperty> properties, MaterialClass materialClass): base(materialDefinitionId)
    {
        Name = name;
        PrimaryUnit = primaryUnit;
        Properties = properties;
        MaterialClass = materialClass;
        MaterialLots = new List<MaterialLot>();
        SecondaryUnits = new List<MaterialUnit>();
        Operations = new List<Operation>();
    }

    public void Update(string name, string primaryUnit, List<MaterialDefinitionProperty> properties, MaterialClass materialClass)
    {
        Name = name;
        PrimaryUnit = primaryUnit;
        Properties = properties;
        MaterialClass = materialClass;
    }

    public void AddMaterialUnit(string unitId, string unitName, decimal conversionValueToPrimaryUnit)
    {
        var materialUnit = new MaterialUnit(unitId, unitName, conversionValueToPrimaryUnit, Id);
        if(SecondaryUnits.Exists(d => d.UnitId == unitId))
        {
            throw new ChildEntityDuplicationException(unitId, materialUnit, ResourceId, this);
        }

        SecondaryUnits.Add(materialUnit);
    }

    public MaterialUnit GetMaterialUnitWithId(string unitId)
    {
        var materialUnit = SecondaryUnits.Find(d => d.UnitId == unitId) ?? throw new ChildEntityNotFoundException(unitId, typeof(MaterialUnit), ResourceId, this);
        return materialUnit;
    }

    public void UpdateMaterialUnit(string unitId, string unitName, decimal conversionValueToPrimaryUnit)
    {
        var materialUnit = GetMaterialUnitWithId(unitId);

        try
        {
            materialUnit.Update(unitName, conversionValueToPrimaryUnit);
        }
        catch (Exception ex)
        {
            throw new DomainException($"MaterialDefinition with id {ResourceId} throw an exception. See inner exception for details.", ex);
        }
    }

    public void RemoveMaterialUnit(string unitId)
    {
        var materialUnit = GetMaterialUnitWithId(unitId);

        try
        {
            SecondaryUnits.Remove(materialUnit);
        }
        catch (Exception ex)
        {
            throw new DomainException($"MaterialDefinition with id {ResourceId} throw an exception. See inner exception for details.", ex);
        }
    }

    public void AddOperation(string operationId, string name, List<string> prerequisiteOperationId, List<OperationEquipmentRequirement> operationEquipmentRequirements)
    {
        List<Operation> prerequisiteOperations = prerequisiteOperationId.ConvertAll(id => GetOperationWithId(id));

        var operation = new Operation(operationId, name, prerequisiteOperations, Id, operationEquipmentRequirements);

        if (Operations.Exists(d => d.OperationId == operationId))
        {
            throw new ChildEntityDuplicationException(operationId, operation, ResourceId, this);
        }

        Operations.Add(operation);
    }

    public Operation GetOperationWithId(string operationId)
    {
        var operation = Operations.Find(d => d.OperationId == operationId) ?? throw new ChildEntityNotFoundException(operationId, typeof(Operation), ResourceId, this);
        return operation;
    }

    public void UpdateOperation(string operationId, string name, List<string> prerequisiteOperationId)
    {
        List<Operation> prerequisiteOperations = new();

        if (prerequisiteOperationId.Count > 0)
        {
            prerequisiteOperations = prerequisiteOperationId.ConvertAll(id => GetOperationWithId(id));
        }

        var operation = GetOperationWithId(operationId);

        try
        {
            operation.Update(name, prerequisiteOperations);
        }
        catch (Exception ex)
        {
            throw new DomainException($"MaterialDefinition with id {ResourceId} throw an exception. See inner exception for details.", ex);
        }
    }

    public void RemoveOperation(string operationId)
    {
        var operation = GetOperationWithId(operationId);

        try
        {
            Operations.Remove(operation);
        }
        catch (Exception ex)
        {
            throw new DomainException($"MaterialDefinition with id {ResourceId} throw an exception. See inner exception for details.", ex);
        }
    }
}
