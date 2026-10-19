using Application.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Offices.Commands;

public class DeleteOfficeCommand : IRequest<BaseApiResponse<bool>>
{
    public required int Id { get; set; }
}
public class DeleteOfficeCommandHandler(IUnitOfWork UnitOfWork, ILogger<DeleteOfficeCommandHandler> Logger) : IRequestHandler<DeleteOfficeCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Deleting {Office} With Id {Id}", nameof(Office), request.Id);
        var model = await UnitOfWork.Offices.GetById(request.Id);
        if (model == null)
            throw new NotFoundException(nameof(Office), request.Id);
        await UnitOfWork.Offices.Remove(model.Id);
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Office)} deleted successfully") : BaseApiResponse<bool>.Fail("Failed to delete Office");
    }
}