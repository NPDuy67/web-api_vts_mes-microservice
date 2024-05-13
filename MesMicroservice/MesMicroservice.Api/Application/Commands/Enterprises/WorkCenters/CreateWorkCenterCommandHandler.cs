﻿using MesMicroservice.Api.Application.Exceptions;
using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;

namespace MesMicroservice.Api.Application.Commands.Enterprises.WorkCenters;

public class CreateWorkCenterCommandHandler : IRequestHandler<CreateWorkCenterCommand, bool>
{
    private readonly IEnterpriseRepository _enterpriseRepository;

    public CreateWorkCenterCommandHandler(IEnterpriseRepository enterpriseRepository)
    {
        _enterpriseRepository = enterpriseRepository;
    }

    public async Task<bool> Handle(CreateWorkCenterCommand request, CancellationToken cancellationToken)
    {
        var enterprise = await _enterpriseRepository.GetAsync(request.EnterpriseId) ?? throw new ResourceNotFoundException(nameof(Enterprise), request.EnterpriseId);
        var area = enterprise.Sites
            .SelectMany(x => x.Areas)
            .FirstOrDefault(x => x.AbsolutePath == $"{request.EnterpriseId}/{request.SiteId}/{request.AreaId}") ?? throw new ResourceNotFoundException(nameof(Area), request.AreaId);
        var workCenter = new WorkCenter(request.WorkCenterId, request.Name, request.WorkCenterType);

        area.AddWorkCenter(workCenter);

        return await _enterpriseRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
