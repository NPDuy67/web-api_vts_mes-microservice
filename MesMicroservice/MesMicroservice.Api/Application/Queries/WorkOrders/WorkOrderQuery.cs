﻿namespace MesMicroservice.Api.Application.Queries.WorkOrders;

public class WorkOrderQuery: IRequest<WorkOrderViewModel?>
{
    public string ManufacturingOrderId { get; set; }
    public string WorkOrderId { get; set; }

    public WorkOrderQuery(string manufacturingOrderId, string workOrderId)
    {
        ManufacturingOrderId = manufacturingOrderId;
        WorkOrderId = workOrderId;
    }
}
