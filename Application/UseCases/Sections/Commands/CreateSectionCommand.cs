using Application.DTOs.Response;
using Application.DTOs.Sections;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sections.Commands;

public class CreateSectionCommand : IRequest<BaseApiResponse<bool>>
{
    public required SectionDto Dto { get; set; }
}

public class CreateSectionCommandHandler(IUnitOfWork UnitOfWork, ILogger<CreateSectionCommandHandler> Logger) : IRequestHandler<CreateSectionCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        var course = await UnitOfWork.Courses.GetById(request.Dto.CourseId);
        if (course == null)
            return BaseApiResponse<bool>.Fail("Course not found");
        var instructor = await UnitOfWork.Instructors.GetById(request.Dto.InstructorId ?? -1);
        if (instructor == null)
            return BaseApiResponse<bool>.Fail("Instructor not found");
        var schedule = await UnitOfWork.Schedules.GetById(request.Dto.ScheduleId);
        if (schedule == null)
            return BaseApiResponse<bool>.Fail("Schedule not found");
        Logger.LogInformation("Creating {Section} With Name {Name}", nameof(Section), request.Dto.Name);
        await UnitOfWork.Sections.Add(request.Dto.ToEntit());
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Section)} created successfully") : BaseApiResponse<bool>.Fail("Failed to create section");
    }
}
