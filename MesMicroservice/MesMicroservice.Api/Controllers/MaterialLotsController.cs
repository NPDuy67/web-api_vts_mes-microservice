using MesMicroservice.Api.Application.Queries.MaterialLots;
using Microsoft.AspNetCore.Mvc;

namespace MesMicroservice.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MaterialLotsController : ApiControllerBase
{
    public MaterialLotsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<QueryResult<MaterialLotViewModel>> GetMaterialLots([FromQuery] MaterialLotsQuery query)
    {
        return await _mediator.Send(query);
    }
}
