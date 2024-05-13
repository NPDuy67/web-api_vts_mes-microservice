using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;
public class ManufacturingOrder : Entity, IAggregateRoot
{
    public string ManufacturingOrderId { get; private set; }
    public MaterialDefinition MaterialDefinition { get; private set; }
    public decimal Quantity { get; private set; }
    public string Unit { get; private set; }
    public DateTime AvailableDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public List<WorkOrder> WorkOrders { get; private set; }
    public int Priority { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ManufacturingOrder() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ManufacturingOrder(string manufacturingOrderId, MaterialDefinition materialDefinition, decimal quantity, string unit, DateTime dueDate, DateTime availableDate, int priority)
    {
        ManufacturingOrderId = manufacturingOrderId;
        MaterialDefinition = materialDefinition;
        Quantity = quantity;
        Unit = unit;
        DueDate = dueDate;
        WorkOrders = new List<WorkOrder>();
        AvailableDate = availableDate;
        Priority = priority;
    }

    public void Update(MaterialDefinition materialDefinition, decimal quantity, string unit, DateTime dueDate, DateTime availableDate, int priority)
    {
        MaterialDefinition = materialDefinition;
        Quantity = quantity;
        Unit = unit;
        DueDate = dueDate;
        AvailableDate = availableDate;
        Priority = priority;
    }

    public void AddWorkOrder(WorkOrder workOrder)
    {
        if (WorkOrders.Exists(d => d.WorkOrderId == workOrder.WorkOrderId))
        {
            throw new ChildEntityDuplicationException(workOrder.WorkOrderId, workOrder, ManufacturingOrderId, this);
        }

        WorkOrders.Add(workOrder);
    }

    public WorkOrder GetWorkOrderWithId(string workOrderId)
    {
        var workOrder = WorkOrders.Find(d => d.WorkOrderId == workOrderId);
        return workOrder ?? throw new ChildEntityNotFoundException(workOrderId, typeof(WorkOrder), ManufacturingOrderId, this);
    }

    public void RemoveWorkOrder(string workOrderId)
    {
        var workOrder = GetWorkOrderWithId(workOrderId);

        try
        {
            WorkOrders.Remove(workOrder);
        }
        catch (Exception ex)
        {
            throw new DomainException($"ManufacturingOrder with id {ManufacturingOrderId} throw an exception. See inner exception for details.", ex);
        }
    }
}
