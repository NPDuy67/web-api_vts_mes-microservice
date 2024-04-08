using MesMicroservice.Domain.AggregateModels.MaterialClassAggregate;

namespace MesMicroservice.Api.Application.Mapping.Converters;

public class MaterialClassToStringConverter : ITypeConverter<MaterialClass, string>
{
    public string Convert(MaterialClass source, string destination, ResolutionContext context)
    {
        return source.ResourceId;
    }
}
