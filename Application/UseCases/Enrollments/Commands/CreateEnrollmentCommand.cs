using Application.DTOs.Enrollments;
using Application.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Enrollments;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Enrollments.Commands;

public class CreateEnrollmentCommand : IRequest<BaseApiResponse<bool>>
{
    public required EnrollmentDto Dto { get; set; }
}

public class CreateEnrollmentCommandHandler(IUnitOfWork UnitOfWork, ILogger<CreateEnrollmentCommandHandler> Logger) : IRequestHandler<CreateEnrollmentCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Creating {Enrollment} for SectionId {SectionId} and StudentId {StudentId}", nameof(Enrollment), request.Dto.SectionId, request.Dto.StudentId);
        var student = await UnitOfWork.Students.GetById(request.Dto.StudentId);
        if (student == null) return BaseApiResponse<bool>.Fail("Studet not found");
        var section = await UnitOfWork.Sections.GetById(request.Dto.SectionId);
        if (section == null) return BaseApiResponse<bool>.Fail("Section not found");
        var enrollments = await UnitOfWork.Enrollments.GetAllWithSpec(new GetEnrollmentSpec2(student.Id, section.Id));
        if (enrollments.Count > 0) return BaseApiResponse<bool>.Fail("You are already enrolled in this section");
        var enrollment = new Enrollment
        {
            StudentId = student.Id,
            SectionId = section.Id
        };
        await UnitOfWork.Enrollments.Add(enrollment);
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Enrollment)} created successfully") : BaseApiResponse<bool>.Fail("Failed to create enrollment");
    }
}
