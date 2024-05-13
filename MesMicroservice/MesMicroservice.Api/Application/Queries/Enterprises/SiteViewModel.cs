namespace MesMicroservice.Api.Application.Queries.Enterprises;

public class SiteViewModel
{
    public string SiteId { get; set; }
    public string Name { get; set; }
    public string AbsolutePath { get; set; }
    public List<AreaViewModel> Areas { get; set; }

    public SiteViewModel(string siteId, string name, string absolutePath, List<AreaViewModel> areas)
    {
        SiteId = siteId;
        Name = name;
        AbsolutePath = absolutePath;
        Areas = areas;
    }
}
