using MesMicroservice.Api.Application.Commands.MaterialClasses;
using MesMicroservice.Api.Application.Queries.MaterialClasses;
using Microsoft.AspNetCore.Mvc;

namespace MesMicroservice.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MaterialClassesController : ApiControllerBase
{
    public MaterialClassesController(IMediator mediator) : base(mediator)
    { 
    }

    [HttpPost]
    public async Task<IActionResult> CreateMaterialClass([FromBody] CreateMaterialClassCommand command)
    {
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{materialClassId}")]
    public async Task<IActionResult> UpdateMaterialClass([FromRoute] string materialClassId, [FromBody] UpdateMaterialClassViewModel materialClass)
    {
        var command = new UpdateMaterialClassCommand(materialClassId, materialClass.Name);
        return await CommandAsync(command);
    }

    [HttpGet]
    public async Task<QueryResult<MaterialClassViewModel>> GetMaterialClasses([FromQuery] MaterialClassesQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{materialClassId}")]
    public async Task<IActionResult> DeleteMaterialClass([FromRoute] string materialClassId)
    {
        var command = new DeleteMaterialClassCommand(materialClassId);
        return await CommandAsync(command);
    }
}
