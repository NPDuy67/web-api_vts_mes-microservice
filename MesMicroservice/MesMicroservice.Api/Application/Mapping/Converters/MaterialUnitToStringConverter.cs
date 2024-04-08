using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;

namespace MesMicroservice.Api.Application.Mapping.Converters;
public class MaterialUnitToStringConverter : ITypeConverter<MaterialUnit, string>
{
    public string Convert(MaterialUnit source, string destination, ResolutionContext context)
    {
        return source.UnitId;
    }
}
