namespace MesMicroservice.Api.Application.Commands.Enterprises.Sites;

[DataContract]
public class CreateSiteViewModel
{
    [DataMember]
    public string SiteId { get; set; }
    [DataMember]
    public string Name { get; set; }

    public CreateSiteViewModel(string siteId, string name)
    {
        SiteId = siteId;
        Name = name;
    }
}
