﻿using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Api.Application.Commands.WorkOrders;
[DataContract]
public class UpdateWorkOrderViewModel
{
    [DataMember]
    public DateTime StartTime { get; set; }
    [DataMember]
    public DateTime EndTime { get; set; }
    [DataMember]
    public DateTime? ActuallyStartTime { get; set; }
    [DataMember]
    public DateTime? ActuallyEndTime { get; set; }
    [DataMember]
    public EWorkOrderStatus WorkOrderStatus { get; set; }
    [DataMember]
    public string WorkCenter { get; set; }

    public UpdateWorkOrderViewModel(DateTime startTime, DateTime endTime, DateTime? actuallyStartTime, DateTime? actuallyEndTime, EWorkOrderStatus workOrderStatus, string workCenter)
    {
        StartTime = startTime;
        EndTime = endTime;
        ActuallyStartTime = actuallyStartTime;
        ActuallyEndTime = actuallyEndTime;
        WorkOrderStatus = workOrderStatus;
        WorkCenter = workCenter;
    }
}
