namespace MesMicroservice.Api.Application.Queries.WorkOrders;

public class DownloadReportQuery : IRequest<byte[]>
{
    public string ManufacturingOrderId { get; set; }
    public string WorkOrderId { get; set; }

    public DownloadReportQuery(string manufacturingOrderId, string workOrderId)
    {
        ManufacturingOrderId = manufacturingOrderId;
        WorkOrderId = workOrderId;
    }
}
