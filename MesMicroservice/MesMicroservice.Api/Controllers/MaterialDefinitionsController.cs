using MesMicroservice.Api.Application.Commands.MaterialDefinitions;
using MesMicroservice.Api.Application.Commands.MaterialDefinitions.MaterialUnits;
using MesMicroservice.Api.Application.Commands.MaterialDefinitions.Operations;
using MesMicroservice.Api.Application.Queries.MaterialDefinitions;
using Microsoft.AspNetCore.Mvc;

namespace MesMicroservice.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MaterialDefinitionsController : ApiControllerBase
{
    public MaterialDefinitionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateMaterialDefinition([FromBody] CreateMaterialDefinitionCommand command)
    {
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{materialDefinitionId}")]
    public async Task<IActionResult> UpdateMaterialDefinition([FromRoute] string materialDefinitionId, [FromBody] UpdateMaterialDefinitionViewModel materialDefinition)
    {
        var command = new UpdateMaterialDefinitionCommand(materialDefinitionId, materialDefinition.Name, materialDefinition.PrimaryUnit, materialDefinition.Properties, materialDefinition.MaterialClass);
        return await CommandAsync(command);
    }

    [HttpGet]
    public async Task<QueryResult<MaterialDefinitionViewModel>> GetMaterialDefinitions([FromQuery] MaterialDefinitionsQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{materialDefinitionId}")]
    public async Task<IActionResult> DeleteMaterialDefinition([FromRoute] string materialDefinitionId)
    {
        var command = new DeleteMaterialDefinitionCommand(materialDefinitionId);
        return await CommandAsync(command);
    }

    [HttpPost]
    [Route("{materialDefinitionId}/materialUnits")]
    public async Task<IActionResult> CreateMaterialUnit([FromRoute] string materialDefinitionId, [FromBody] CreateMaterialUnitViewModel materilUnit)
    {
        var command = new CreateMaterialUnitCommand(materialDefinitionId, materilUnit.UnitId, materilUnit.UnitName, materilUnit.ConversionValueToPrimaryUnit);
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{materialDefinitionId}/materialUnits/{materialUnitId}")]
    public async Task<IActionResult> UpdateMaterialUnit([FromRoute] string materialDefinitionId, [FromRoute] string materialUnitId, [FromBody] UpdateMaterialUnitViewModel materialUnit)
    {
        var command = new UpdateMaterialUnitCommand(materialDefinitionId, materialUnitId, materialUnit.UnitName, materialUnit.ConversionValueToPrimaryUnit);
        return await CommandAsync(command);
    }

    [HttpDelete]
    [Route("{materialDefinitionId}/materialUnits/{materialUnitId}")]
    public async Task<IActionResult> DeleteMaterialUnit([FromRoute] string materialDefinitionId, [FromRoute] string materialUnitId)
    {
        var command = new DeleteMaterialUnitCommand(materialDefinitionId, materialUnitId);
        return await CommandAsync(command);
    }

    [HttpPost]
    [Route("{materialDefinitionId}/operations")]
    public async Task<IActionResult> CreateOperation([FromRoute] string materialDefinitionId, [FromBody] CreateOperationViewModel operation)
    {
        var command = new CreateOperationCommand(materialDefinitionId, operation.OperationId, operation.Name, operation.Duration, operation.PrerequisiteOperation);
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{materialDefinitionId}/operations/{operationId}")]
    public async Task<IActionResult> UpdateOperation([FromRoute] string materialDefinitionId, [FromRoute] string operationId, [FromBody] UpdateOperationViewModel operation)
    {
        var command = new UpdateOperationCommand(materialDefinitionId, operationId, operation.Name, operation.Duration, operation.PrerequisiteOperation);
        return await CommandAsync(command);
    }

    [HttpDelete]
    [Route("{materialDefinitionId}/operations/{operationId}")]
    public async Task<IActionResult> DeleteOperation([FromRoute] string materialDefinitionId, [FromRoute] string operationId)
    {
        var command = new DeleteOperationCommand(materialDefinitionId, operationId);
        return await CommandAsync(command);
    }
}
