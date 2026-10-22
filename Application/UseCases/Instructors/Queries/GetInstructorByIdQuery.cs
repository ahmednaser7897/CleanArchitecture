using Application.DTOs.Instructors;
using Application.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Instructors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Instructors.Queries;

public class GetInstructorByIdQuery(int id) : IRequest<BaseApiResponse<InstructorDto?>>
{
    public int Id { get; set; } = id;
}

public class GetInstructorByIdQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetInstructorByIdQueryHandler> Logger) : IRequestHandler<GetInstructorByIdQuery, BaseApiResponse<InstructorDto?>>
{
    public async Task<BaseApiResponse<InstructorDto?>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting {Instructor} with Id {Id}", nameof(Instructor), request.Id);
        var model = await UnitOfWork.Instructors.GetByIdWithSpec(new GetInstructorSpec(null), request.Id)
        ?? throw new NotFoundException(nameof(Instructor), request.Id);
        return BaseApiResponse<InstructorDto?>.Success(
            data: InstructorDto.FromEntit(model),
            message: $"{nameof(Instructor)} Found Successfully"
        );
    }
}
