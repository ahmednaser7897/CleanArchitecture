using Application.DTOs.Response;
using Application.DTOs.Students;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Students;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Students.Queries;

public class GetStudentByIdQuery(int id) : IRequest<BaseApiResponse<StudentDto?>>
{
    public int Id { get; set; } = id;
}

public class GetStudentByIdQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetStudentByIdQueryHandler> Logger) : IRequestHandler<GetStudentByIdQuery, BaseApiResponse<StudentDto?>>
{
    public async Task<BaseApiResponse<StudentDto?>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting {Student} with Id {Id}", nameof(Student), request.Id);
        var model = await UnitOfWork.Students.GetByIdWithSpec(new GetStudentSpec(null), request.Id)
        ?? throw new NotFoundException(nameof(Student), request.Id);
        return BaseApiResponse<StudentDto?>.Success(
            data: StudentDto.FromEntit(model),
            message: $"{nameof(Student)} Found Successfully"
        );
    }
}
