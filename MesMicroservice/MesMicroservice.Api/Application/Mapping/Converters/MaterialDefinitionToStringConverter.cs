using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;

namespace MesMicroservice.Api.Application.Mapping.Converters;

public class MaterialDefinitionToStringConverter : ITypeConverter<MaterialDefinition, string>
{
    public string Convert(MaterialDefinition source, string destination, ResolutionContext context)
    {
        return source.ResourceId;
    }
}
