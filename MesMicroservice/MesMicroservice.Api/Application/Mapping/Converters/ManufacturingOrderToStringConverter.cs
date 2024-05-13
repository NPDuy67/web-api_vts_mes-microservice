using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;

namespace MesMicroservice.Api.Application.Mapping.Converters;

public class ManufacturingOrderToStringConverter : ITypeConverter<ManufacturingOrder, string>
{
    public string Convert(ManufacturingOrder source, string destination, ResolutionContext context)
    {
        return source.ManufacturingOrderId;
    }
}
