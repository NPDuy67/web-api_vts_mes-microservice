using MesMicroservice.Domain.AggregateModels.EquipmentAggregate;
using MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;
using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialClassAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;
using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;
using ProductionScheduling.Algorithms;
using ProductionScheduling.Algorithms.Models;

List<EquipmentClass> equipmentClasses = new();
using (var reader = new StreamReader(@"D:\Lab\Luận văn\Điều độ\Data\equipmentclasses.csv"))
{
    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        var values = line!.Split(',');

        equipmentClasses.Add(new EquipmentClass(values[0], values[1]));
    }
}

List<Equipment> equipments = new();
using (var reader = new StreamReader(@"D:\Lab\Luận văn\Điều độ\Data\equipments.csv"))
{
    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        var values = line!.Split(',');

        var equipmentClass = equipmentClasses.First(d => d.ResourceId == values[1]);
        var equipment = new Equipment(values[0], "", new(), equipmentClass, null);
        equipments.Add(equipment);
        equipmentClass.Equipments.Add(equipment);
    }
}

var materialClass = new MaterialClass("mc1", "");

var materialDefinitions = new List<MaterialDefinition>();
using (var reader = new StreamReader(@"D:\Lab\Luận văn\Điều độ\Data\materials.csv"))
{
    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        var values = line!.Split(',');

        materialDefinitions.Add(new MaterialDefinition(values[0], "", "unit", new(), materialClass));
    }
}

var manufacturingOrders = new List<ManufacturingOrder>();
using (var reader = new StreamReader(@"D:\Lab\Luận văn\Điều độ\Data\workorders.csv"))
{
    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        var values = line!.Split(',');
        var manufacturingOrder = new ManufacturingOrder(values[0], materialDefinitions.First(d => d.ResourceId == values[1]), decimal.Parse(values[5]), "unit", DateTime.Parse(values[3]), DateTime.Parse(values[2]), 1);
        var requirement = new WorkOrderEquipmentRequirement(equipmentClasses.First(d => d.ResourceId == values[7]), 1);
        manufacturingOrder.AddWorkOrder(new WorkOrder(values[6], DateTime.Parse(values[3]), TimeSpan.FromDays(double.Parse(values[4])), null, null, EWorkOrderStatus.Draft, new(), null, decimal.Parse(values[5]), new() { requirement }));
        manufacturingOrders.Add(manufacturingOrder);
    }
}

var workOrders = manufacturingOrders.SelectMany(x => x.WorkOrders).ToArray();
var equipmentSchedules = equipments.Select(x => new EquipmentSchedule(x, new())).ToList();

workOrders = StaticOrderScheduling.ScheduleBasedOnStaticOrder(workOrders, equipmentSchedules);

foreach (var workOrder in workOrders)
{
    Console.WriteLine($"{workOrder.WorkOrderId}: {workOrder.StartTime} {workOrder.EndTime}");
}