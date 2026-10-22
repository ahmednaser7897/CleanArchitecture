using Application.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sections.Commands;

public class DeleteSectionCommand : IRequest<BaseApiResponse<bool>>
{
    public required int Id { get; set; }
}

public class DeleteSectionCommandHandler(IUnitOfWork UnitOfWork, ILogger<DeleteSectionCommandHandler> Logger) : IRequestHandler<DeleteSectionCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Deleting {Section} With Id {Id}", nameof(Section), request.Id);
        var model = await UnitOfWork.Sections.GetById(request.Id);
        if (model == null)
            throw new NotFoundException(nameof(Section), request.Id);
        await UnitOfWork.Sections.Remove(model.Id);
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Section)} deleted successfully") : BaseApiResponse<bool>.Fail("Failed to delete section");
    }
}
