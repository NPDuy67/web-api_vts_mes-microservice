using MesMicroservice.Domain.AggregateModels.EquipmentAggregate;

namespace MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;
public class EquipmentWorkOrder: IAggregateRoot
{
    public int WorkOrderId { get; private set; }
    public int EquipmentId { get; private set; }
    public WorkOrder WorkOrder { get; private set; }
    public Equipment Equipment { get; private set; }

    public EquipmentWorkOrder(int workOrderId, int equipmentId, WorkOrder workOrder, Equipment equipment)
    {
        WorkOrderId = workOrderId;
        EquipmentId = equipmentId;
        WorkOrder = workOrder;
        Equipment = equipment;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EquipmentWorkOrder() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
