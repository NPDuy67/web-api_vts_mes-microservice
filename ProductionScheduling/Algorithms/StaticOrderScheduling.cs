using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;
using ProductionScheduling.Algorithms.Exceptions;
using ProductionScheduling.Algorithms.Models;

namespace ProductionScheduling.Algorithms;
public static class StaticOrderScheduling
{
    public static WorkOrder[] ScheduleBasedOnStaticOrder(WorkOrder[] workOrders, List<EquipmentSchedule> equipmentSchedules)
    {
        var insufficientEquipmentWorkOrder = workOrders
            .Where(wo => wo.EquipmentRequirements.Exists(
                er => er.Quantity > er.EquipmentClass.Equipments.Count))
            .ToList();

        if (insufficientEquipmentWorkOrder.Any())
        {
            throw new InsufficientEquipmentException(insufficientEquipmentWorkOrder);
        }

        foreach (var workOrder in workOrders)
        {
            var potentialStartDates = equipmentSchedules
                .Where(es => workOrder.EquipmentRequirements
                    .SelectMany(er => er.EquipmentClass.Equipments)
                    .Any(e => e == es.Equipment))
                .SelectMany(es => es.GetPotentialNewStartTimes(workOrder.Duration))
                .ToList();
            potentialStartDates.Add(workOrder.ManufacturingOrder.AvailableDate);
            potentialStartDates.Sort();

            DateTime startTime = potentialStartDates.Last();
            List<EquipmentSchedule> scheduledEquipments = new();
            foreach (var potentialStartDate in potentialStartDates)
            {
                List<EquipmentSchedule> validEquipments = new();
                foreach (var requirement in workOrder.EquipmentRequirements)
                {
                    List<EquipmentSchedule> requirementValidEquipments = new();
                    foreach (var equipment in requirement.EquipmentClass.Equipments)
                    {
                        var equipmentSchedule = equipmentSchedules
                            .First(es => es.Equipment == equipment);
                        if (equipmentSchedule.ValidateNewWorkOrder(potentialStartDate, workOrder.Duration))
                        {
                            requirementValidEquipments.Add(equipmentSchedule);
                        }

                        if (requirementValidEquipments.Count >= requirement.Quantity)
                        {
                            break;
                        }
                    }

                    if (requirementValidEquipments.Count < requirement.Quantity)
                    {
                        break;
                    }
                    else
                    {
                        validEquipments.AddRange(requirementValidEquipments);
                    }
                }

                scheduledEquipments = validEquipments;
            }

            foreach (var equipment in scheduledEquipments)
            {
                equipment.WorkOrders.Add(workOrder);
                workOrder.SetStartTime(startTime);
            }
        }

        return workOrders;
    }
}
