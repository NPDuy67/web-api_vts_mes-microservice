﻿using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Api.Application.Mapping.Converters;

public class WorkOrderToStringConverter : ITypeConverter<WorkOrder, string>
{
    public string Convert(WorkOrder source, string destination, ResolutionContext context)
    {
        return source.WorkOrderId;
    }
}
