namespace MesMicroservice.Api.Application.Queries.Enterprises;

public class AreaViewModel
{
    public string AreaId { get; set; }
    public string Name { get; set; }
    public string AbsolutePath { get; set; }
    public List<WorkCenterViewModel> WorkCenters { get; set; }

    public AreaViewModel(string areaId, string name, string absolutePath, List<WorkCenterViewModel> workCenters)
    {
        AreaId = areaId;
        Name = name;
        AbsolutePath = absolutePath;
        WorkCenters = workCenters;
    }
}
