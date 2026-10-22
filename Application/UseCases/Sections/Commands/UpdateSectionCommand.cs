using Application.DTOs.Response;
using Application.DTOs.Sections;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sections.Commands;

public class UpdateSectionCommand : IRequest<BaseApiResponse<bool>>
{
    public required SectionDto Dto { get; set; }
    public required int Id { get; set; }
}

public class UpdateSectionCommandHandler(IUnitOfWork UnitOfWork, ILogger<UpdateSectionCommandHandler> Logger) : IRequestHandler<UpdateSectionCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Updating {Section} with Id {Id}", nameof(Section), request.Id);
        var model = await UnitOfWork.Sections.GetById(request.Id);
        if (model == null)
            throw new NotFoundException(nameof(Section), request.Id);
        request.Dto.Id = model.Id;
        await UnitOfWork.Sections.Update(request.Dto.ToEntit());
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Section)} updated successfully") : BaseApiResponse<bool>.Fail("Failed to update section");
    }
}
