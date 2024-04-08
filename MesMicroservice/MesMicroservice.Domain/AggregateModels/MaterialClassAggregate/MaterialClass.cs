namespace MesMicroservice.Domain.AggregateModels.MaterialClassAggregate;

public class MaterialClass : Resource, IAggregateRoot
{
    public string Name { get; private set; }
    public List<MaterialDefinition> MaterialDefinitions { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private MaterialClass() : base() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public MaterialClass(string materialClassId, string name) : base(materialClassId)
    {
        Name = name;
        MaterialDefinitions = new List<MaterialDefinition>();
    }

    public void Update(string name)
    {
        Name = name;
    }
}
