using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace ProductionScheduling.Algorithms.Exceptions;
public class InsufficientEquipmentException: Exception
{
    public List<WorkOrder> WorkOrders { get; private set; }

    public InsufficientEquipmentException(List<WorkOrder> workOrders)
    {
        WorkOrders = workOrders;
    }
}
