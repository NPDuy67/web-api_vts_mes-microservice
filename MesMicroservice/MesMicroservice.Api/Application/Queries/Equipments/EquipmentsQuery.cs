namespace MesMicroservice.Api.Application.Queries.Equipments;

public class EquipmentsQuery : PaginatedQuery, IRequest<QueryResult<EquipmentViewModel>>
{
    public string? IdStartedWith { get; set; }
}
