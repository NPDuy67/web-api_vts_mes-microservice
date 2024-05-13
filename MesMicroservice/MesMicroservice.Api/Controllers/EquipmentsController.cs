using MesMicroservice.Api.Application.Commands.Equipments;
using MesMicroservice.Api.Application.Queries.Equipments;
using Microsoft.AspNetCore.Mvc;

namespace MesMicroservice.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EquipmentsController : ApiControllerBase
{
    public EquipmentsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateEquipment([FromBody] CreateEquipmentCommand command)
    {
        return await CommandAsync(command);
    }

    [HttpPut]
    [Route("{equipmentId}")]
    public async Task<IActionResult> UpdateEquipment([FromRoute] string equipmentId, [FromBody] UpdateEquipmentViewModel equipment)
    {
        var command = new UpdateEquipmentCommand(equipmentId, equipment.Name, equipment.Properties, equipment.EquipmentClass, equipment.AbsolutePath);
        return await CommandAsync(command);
    }

    [HttpGet]
    public async Task<QueryResult<EquipmentViewModel>> GetEquipments([FromQuery] EquipmentsQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{equipmentId}")]
    public async Task<IActionResult> DeleteEquipment([FromRoute] string equipmentId)
    {
        var command = new DeleteEquipmentCommand(equipmentId);
        return await CommandAsync(command);
    }

    [HttpGet]
    [Route("{equipmentId}")]
    public async Task<EquipmentViewModel?> GetEquipment([FromRoute] string equipmentId)
    {
        var query = new EquipmentQuery(equipmentId);
        return await _mediator.Send(query);
    }

    [HttpGet]
    [Route("oees")]
    public async Task<IEnumerable<OeeViewModel>> GetOees([FromQuery] OeeQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet]
    [Route("ids")]
    public async Task<IEnumerable<EquipmentIdViewModel>> GetIds()
    {
        return await _mediator.Send(new EquipmentIdQuery());
    }
}
