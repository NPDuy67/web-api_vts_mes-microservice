﻿using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;

namespace MesMicroservice.Api.Application.Commands.Enterprises.WorkCenters;

[DataContract]
public class CreateWorkCenterViewModel
{
    [DataMember]
    public string WorkCenterId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public EWorkCenterType WorkCenterType { get; set; }

    public CreateWorkCenterViewModel(string workCenterId, string name, EWorkCenterType workCenterType)
    {
        WorkCenterId = workCenterId;
        Name = name;
        WorkCenterType = workCenterType;
    }
}
