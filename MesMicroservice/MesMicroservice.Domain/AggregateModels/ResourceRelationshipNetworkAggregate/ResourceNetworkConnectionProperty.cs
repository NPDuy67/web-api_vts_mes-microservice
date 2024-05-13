﻿namespace MesMicroservice.Domain.AggregateModels.ResourceRelationshipNetworkAggregate;
public class ResourceNetworkConnectionProperty : Entity
{
    public int ConnectionId { get; private set; }
    public string PropertyId { get; private set; }
    public string Description { get; private set; }
    public Value Value { get; private set; }
    public string ValueUnitOfMeasure { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ResourceNetworkConnectionProperty() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ResourceNetworkConnectionProperty(string propertyId, string description, Value value, string valueUnitOfMeasure)
    {
        PropertyId = propertyId;
        Description = description;
        Value = value;
        ValueUnitOfMeasure = valueUnitOfMeasure;
    }
}
