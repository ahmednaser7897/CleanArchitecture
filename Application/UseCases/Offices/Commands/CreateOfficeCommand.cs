using Application.DTOs.Offices;
using Application.DTOs.Response;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Offices.Commands;

public class CreateOfficeCommand : IRequest<BaseApiResponse<bool>>
{
    public required OfficeDto Dto { get; set; }
}
public class CreateOfficeCommandHandler(IUnitOfWork UnitOfWork, ILogger<CreateOfficeCommandHandler> Logger) : IRequestHandler<CreateOfficeCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Creating {Office} With Name {Name}", nameof(Office), request.Dto.Name);
        await UnitOfWork.Offices.Add(request.Dto.ToEntit());
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Office)} created successfully") : BaseApiResponse<bool>.Fail("Failed to create Office");
    }
}