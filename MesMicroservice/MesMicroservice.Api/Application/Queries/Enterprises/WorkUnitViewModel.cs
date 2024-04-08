namespace MesMicroservice.Api.Application.Queries.Enterprises;

public class WorkUnitViewModel
{
    public string WorkUnitId { get; set; }
    public string Name { get; set; }
    public string AbsolutePath { get; set; }

    public WorkUnitViewModel(string workUnitId, string name, string absolutePath)
    {
        WorkUnitId = workUnitId;
        Name = name;
        AbsolutePath = absolutePath;
    }
}
