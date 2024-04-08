using MesMicroservice.Api.Application.Commands.ManufacturingRecords;
using MesMicroservice.Api.Application.Queries.ManufacturingRecords;
using Microsoft.AspNetCore.Mvc;

namespace MesMicroservice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManufacturingRecordsController : ApiControllerBase
{
    public ManufacturingRecordsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateManufacturingRecord([FromBody] CreateManufacturingRecordCommand command)
    {
        return await CommandAsync(command);
    }

    [HttpGet]
    public async Task<QueryResult<ManufacturingRecordViewModel>> Get([FromQuery] ManufacturingRecordsQuery query)
    {
        return await _mediator.Send(query);
    }
}
