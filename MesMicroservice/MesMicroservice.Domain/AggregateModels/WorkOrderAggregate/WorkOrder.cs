using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;
using MesMicroservice.Domain.AggregateModels.ManufacucturingRecordAggregate;

namespace MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;
public class WorkOrder : Entity, IAggregateRoot
{
    public string WorkOrderId { get; private set; }
    public DateTime DueDate { get; private set; }
    public TimeSpan Duration { get; private set; }
    public DateTime? StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public DateTime? ActuallyStartTime { get; private set; }
    public DateTime? ActuallyEndTime { get; private set; }
    public EWorkOrderStatus WorkOrderStatus { get; private set; }
    public List<WorkOrder> PrerequisiteOperations { get; private set; }
    public WorkCenter? WorkCenter { get; private set; }
    public ManufacturingOrder ManufacturingOrder { get; private set; }
    public int ManufacturingOrderId { get; private set; }
    public decimal ProductionTarget { get; private set; }
    public decimal ActualQuantity => ManufacturingRecords.Sum(x => x.Output);
    public decimal Progress => ProductionTarget == 0 ? 0 : ActualQuantity / ProductionTarget;
    public List<ManufacturingRecord> ManufacturingRecords { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WorkOrder() { }

    public WorkOrder(string workOrderId, DateTime dueDate, TimeSpan duration, DateTime? startTime, DateTime? endTime, EWorkOrderStatus workOrderStatus, List<WorkOrder> prerequisiteOperations, WorkCenter? workCenter, decimal productionTarget)
    {
        WorkOrderId = workOrderId;
        DueDate = dueDate;
        Duration = duration;
        StartTime = startTime;
        EndTime = endTime;
        WorkOrderStatus = workOrderStatus;
        PrerequisiteOperations = prerequisiteOperations;
        WorkCenter = workCenter;
        ProductionTarget = productionTarget;
        ManufacturingRecords = new List<ManufacturingRecord>();
    }

    public WorkOrder(string workOrderId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        WorkOrderId = workOrderId;
    }

    public void SetPrerequisiteOperations(List<WorkOrder> prerequisiteOperations)
    {
        PrerequisiteOperations = prerequisiteOperations;
    }

    public void SetDuration(TimeSpan duration)
    {
        Duration = duration;
    }

    public void SetDueDate(DateTime dueDate)
    {
        DueDate = dueDate;
    }

    public void SetProductionTarget(decimal productionTarget)
    {
        ProductionTarget = productionTarget;
    }

    public void Update(TimeSpan duration, DateTime? startTime, DateTime? endTime, DateTime? actuallyStartTime, DateTime? actuallyEndTime, EWorkOrderStatus workOrderStatus, WorkCenter? workCenter)
    {
        Duration = duration;
        StartTime = startTime;
        EndTime = endTime;
        ActuallyStartTime = actuallyStartTime;
        ActuallyEndTime = actuallyEndTime;
        WorkOrderStatus = workOrderStatus;
        WorkCenter = workCenter;
    }
}
