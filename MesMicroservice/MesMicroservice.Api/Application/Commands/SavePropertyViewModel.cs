namespace MesMicroservice.Api.Application.Commands;

public class SavePropertyViewModel
{
    public string PropertyId { get; set; }
    public string Description { get; set; }
    public string ValueString { get; set; } 
    public EValueType ValueType { get; set; }
    public string ValueUnitOfMeasure { get; set; }

    public SavePropertyViewModel(string propertyId, string description, string valueString, EValueType valueType, string valueUnitOfMeasure)
    {
        PropertyId = propertyId;
        Description = description;
        ValueString = valueString;
        ValueType = valueType;
        ValueUnitOfMeasure = valueUnitOfMeasure;
    }
}
