using MesMicroservice.Domain.AggregateModels.EquipmentAggregate;
using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace ProductionScheduling.Algorithms.Models;
public class EquipmentSchedule
{
    public Equipment Equipment { get; set; }
    public List<WorkOrder> WorkOrders { get; set; }

    public EquipmentSchedule(Equipment equipment, List<WorkOrder> workOrders)
    {
        Equipment = equipment;
        WorkOrders = workOrders;
    }

    public List<DateTime> GetPotentialNewStartTimes(TimeSpan duration)
    {
        return WorkOrders.Select(wo => wo.EndTime!.Value)
                         .Where(x => ValidateNewWorkOrder(x, duration))
                         .ToList();
    }

    public bool ValidateNewWorkOrder(DateTime startTime, TimeSpan duration)
    {
        var endTime = startTime + duration;
        foreach (var workOrder in WorkOrders)
        {
            if ((startTime < workOrder.EndTime && startTime > workOrder.StartTime)
                || (endTime < workOrder.EndTime && endTime > workOrder.StartTime))
            {
                return false;
            }
        }
        return true;
    }
}
