using MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;

namespace MesMicroservice.Api.Application.Commands.EquipmentClasses;

public class CreateEquipmentClassCommandHandler : IRequestHandler<CreateEquipmentClassCommand, bool>
{
    private readonly IEquipmentClassRepository _equipmentClassRepository;

    public CreateEquipmentClassCommandHandler(IEquipmentClassRepository equipmentClassRepository)
    {
        _equipmentClassRepository = equipmentClassRepository;
    }

    public async Task<bool> Handle(CreateEquipmentClassCommand request, CancellationToken cancellationToken)
    {
        var equipmentClass = new EquipmentClass(request.EquipmentClassId, request.Name);

        await _equipmentClassRepository.AddAsync(equipmentClass);
        return await _equipmentClassRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
