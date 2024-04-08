namespace MesMicroservice.Api.Application.Messages.ErrorDetails;

public class WorkOrderNotScheduledErrorDetail
{
    public string WorkOrderId { get; set; }

    public WorkOrderNotScheduledErrorDetail(string workOrderId)
    {
        WorkOrderId = workOrderId;
    }
}
