using Application.DTOs.Offices;
using Application.DTOs.Response;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Entities;

namespace Application.UseCases.Offices.Queries;

public class GetOfficeByIdQuery(int id) : IRequest<BaseApiResponse<OfficeDto?>>
{
    public int Id { get; set; } = id;
}
public class GetOfficeByIdQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetOfficeByIdQueryHandler> Logger) : IRequestHandler<GetOfficeByIdQuery, BaseApiResponse<OfficeDto?>>
{
    public async Task<BaseApiResponse<OfficeDto?>> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting {Office} with Id {Id}", nameof(Office), request.Id);
        var model = await UnitOfWork.Offices.GetById(request.Id)
        ?? throw new NotFoundException(nameof(Office), request.Id);
        return BaseApiResponse<OfficeDto?>.Success(
            data: OfficeDto.FromEntit(model),
            message: $"{nameof(Office)} Found Successfully"
        );
    }
}

