namespace MesMicroservice.Api.Application.Queries.MaterialClasses;

public class MaterialClassViewModel
{
    public string MaterialClassId { get; set; }
    public string Name { get; set; }
    public List<string> MaterialDefinitions { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private MaterialClassViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public MaterialClassViewModel(string materialClassId, string name, List<string> materialDefinitions)
    {
        MaterialClassId = materialClassId;
        Name = name;
        MaterialDefinitions = materialDefinitions;
    }
}
