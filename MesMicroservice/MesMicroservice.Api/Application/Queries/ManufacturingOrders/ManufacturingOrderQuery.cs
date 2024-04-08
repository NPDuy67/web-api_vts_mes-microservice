namespace MesMicroservice.Api.Application.Queries.ManufacturingOrders;

public class ManufacturingOrderQuery: IRequest<ManufacturingOrderViewModel?>
{
    public string ManufacturingOrderId { get; set; }

    public ManufacturingOrderQuery(string manufacturingOrderId)
    {
        ManufacturingOrderId = manufacturingOrderId;
    }
}
