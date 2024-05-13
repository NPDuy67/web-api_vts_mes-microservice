namespace MesMicroservice.Api.Application.Commands.MaterialClasses;

[DataContract]
public class UpdateMaterialClassViewModel
{
    [DataMember]
    public string Name { get; set; }

    public UpdateMaterialClassViewModel(string name)
    {
        Name = name;
    }
}
