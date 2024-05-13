using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;

namespace MesMicroservice.Api.Application.Mapping.Converters.EnterpriseConverter;

public class WorkUnitToStringConverter : ITypeConverter<WorkUnit, string>
{
    public string Convert(WorkUnit source, string destination, ResolutionContext context)
    {
        return source.AbsolutePath;
    }
}
