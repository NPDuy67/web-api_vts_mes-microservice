namespace MesMicroservice.Api.Application.Queries.Enterprises;

public class EnterpriseViewModel
{
    public string EnterpriseId { get; set; }
    public string Name { get; set; }
    public string AbsolutePath { get; set; }
    public List<SiteViewModel> Sites { get; set; }

    public EnterpriseViewModel(string enterpriseId, string name, string absolutePath, List<SiteViewModel> sites)
    {
        EnterpriseId = enterpriseId;
        Name = name;
        AbsolutePath = absolutePath;
        Sites = sites;
    }
}

    
