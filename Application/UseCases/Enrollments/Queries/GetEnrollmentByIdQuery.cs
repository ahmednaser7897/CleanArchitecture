using Application.DTOs.Enrollments;
using Application.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Enrollments;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Enrollments.Queries;

public class GetEnrollmentByIdQuery(int id) : IRequest<BaseApiResponse<EnrollmentDto?>>
{
    public int Id { get; set; } = id;
}

public class GetEnrollmentByIdQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetEnrollmentByIdQueryHandler> Logger) : IRequestHandler<GetEnrollmentByIdQuery, BaseApiResponse<EnrollmentDto?>>
{
    public async Task<BaseApiResponse<EnrollmentDto?>> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting {Enrollment} with Id {Id}", nameof(Enrollment), request.Id);
        var model = await UnitOfWork.Enrollments.GetByIdWithSpec(new GetEnrollmentSpec(null), request.Id)
        ?? throw new NotFoundException(nameof(Enrollment), request.Id);
        return BaseApiResponse<EnrollmentDto?>.Success(
            data: EnrollmentDto.FromEntit(model),
            message: $"{nameof(Enrollment)} Found Successfully"
        );
    }
}
