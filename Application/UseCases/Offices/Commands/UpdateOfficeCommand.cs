using Application.DTOs.Offices;
using Application.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Offices.Commands;

public class UpdateOfficeCommand : IRequest<BaseApiResponse<bool>>
{
    public required OfficeDto Dto { get; set; }
    public required int Id { get; set; }

}
public class UpdateOfficeCommandHandler(IUnitOfWork UnitOfWork, ILogger<UpdateOfficeCommandHandler> Logger) : IRequestHandler<UpdateOfficeCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Updating {Office} with Id {Id}", nameof(Office), request.Id);
        var model = await UnitOfWork.Offices.GetById(request.Id);
        if (model == null)
            throw new NotFoundException(nameof(Office), request.Id);
        request.Dto.Id = model.Id;
        await UnitOfWork.Offices.Update(request.Dto.ToEntit());
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Office)} updated successfully") : BaseApiResponse<bool>.Fail("Failed to update Office");
    }
}