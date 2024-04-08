using MesMicroservice.Api.Application.Queries.MaterialDefinitions;

namespace MesMicroservice.Api.Application.Queries.ManufacturingOrders;

public class ManufacturingOrderViewModel
{
    public string ManufacturingOrderId { get; set; }
    public MaterialDefinitionViewModel MaterialDefinition { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime AvailableDate { get; set; }
    public List<string> WorkOrders { get; set; }
    public int Priority { get; set; }

    public ManufacturingOrderViewModel(string manufacturingOrderId, MaterialDefinitionViewModel materialDefinition, decimal quantity, string unit, DateTime dueDate, List<string> workOrders, DateTime availableDate, int priority)
    {
        ManufacturingOrderId = manufacturingOrderId;
        MaterialDefinition = materialDefinition;
        Quantity = quantity;
        Unit = unit;
        DueDate = dueDate;
        WorkOrders = workOrders;
        AvailableDate = availableDate;
        Priority = priority;
    }
}
