using Application.DTOs.Response;
using Application.DTOs.Sections;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Sections;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sections.Queries;

public class GetSectionByIdQuery(int id) : IRequest<BaseApiResponse<SectionDto?>>
{
    public int Id { get; set; } = id;
}

public class GetSectionByIdQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetSectionByIdQueryHandler> Logger) : IRequestHandler<GetSectionByIdQuery, BaseApiResponse<SectionDto?>>
{
    public async Task<BaseApiResponse<SectionDto?>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting {Section} with Id {Id}", nameof(Section), request.Id);
        var model = await UnitOfWork.Sections.GetByIdWithSpec(new GetSectionSpec(null), request.Id)
            ?? throw new NotFoundException(nameof(Section), request.Id);
        return BaseApiResponse<SectionDto?>.Success(
            data: SectionDto.FromEntit(model),
            message: $"{nameof(Section)} Found Successfully"
        );
    }
}
