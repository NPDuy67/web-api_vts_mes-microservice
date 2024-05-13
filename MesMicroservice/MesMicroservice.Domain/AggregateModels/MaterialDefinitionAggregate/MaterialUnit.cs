﻿using System.Runtime.CompilerServices;

namespace MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;
public class MaterialUnit : Entity
{
    public int MaterialDefinitionId { get; set; }
    public string UnitId { get; private set; }
    public string UnitName { get; private set; }
    public decimal ConversionValueToPrimaryUnit { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private MaterialUnit() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public MaterialUnit(string unitId, string unitName, decimal conversionValueToPrimaryUnit, int materialDefinitionId)
    {
        UnitId = unitId;
        UnitName = unitName;
        ConversionValueToPrimaryUnit = conversionValueToPrimaryUnit;
        MaterialDefinitionId = materialDefinitionId;
    }

    public void Update(string unitName, decimal conversionValueToPrimaryUnit)
    {
        UnitName = unitName;
        ConversionValueToPrimaryUnit = conversionValueToPrimaryUnit;
    }
}
