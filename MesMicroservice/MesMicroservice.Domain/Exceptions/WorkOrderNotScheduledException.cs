using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;
using System.Runtime.Serialization;

namespace MesMicroservice.Domain.Exceptions;
[Serializable]
public class WorkOrderNotScheduledException : Exception
{
    public readonly string WorkOrderId = "";

    public WorkOrderNotScheduledException() : base()
    {
    }

    public WorkOrderNotScheduledException(string? message) : base(message)
    {
    }

    public WorkOrderNotScheduledException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected WorkOrderNotScheduledException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public WorkOrderNotScheduledException(WorkOrder workOrder) : base($"Work order {workOrder.WorkOrderId} is not scheduled")
    {
        WorkOrderId = workOrder.WorkOrderId;
    }
}
