using MesMicroservice.Domain.AggregateModels.MaterialClassAggregate;

namespace MesMicroservice.Api.Application.Commands.MaterialClasses;

public class CreateMaterialClassCommandHandler : IRequestHandler<CreateMaterialClassCommand, bool>
{
    private readonly IMaterialClassRepository _materialClassRepository;

    public CreateMaterialClassCommandHandler(IMaterialClassRepository materialClassRepository)
    {
        _materialClassRepository = materialClassRepository;
    }

    public async Task<bool> Handle(CreateMaterialClassCommand request, CancellationToken cancellationToken)
    {
        var materialClass = new MaterialClass(request.MaterialClassId, request.Name);

        await _materialClassRepository.AddAsync(materialClass);
        return await _materialClassRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
