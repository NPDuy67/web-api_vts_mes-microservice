namespace MesMicroservice.Api.Application.Commands.Enterprises.Sites;

public class CreateSiteCommand : IRequest<bool>
{
    public string EnterpriseId { get; set; }
    public string SiteId { get; set; }
    public string Name { get; set; }

    public CreateSiteCommand(string enterpriseId, string siteId, string name)
    {
        EnterpriseId = enterpriseId;
        SiteId = siteId;
        Name = name;
    }
}
