using Application.DTOs.Courses;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Application.UseCases.Course.Commands;

public class CreateCourseCommand : IRequest<AddCourseDto>
{
    public required AddCourseDto CourseDto { get; set; }
}
public class CreateCourseCommandHandler(IUnitOfWork UnitOfWork) : IRequestHandler<CreateCourseCommand, AddCourseDto>
{
    public async Task<AddCourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Domain.Entities.Course
        {
            Name = request.CourseDto.Name,
            Price = request.CourseDto.Price,
        };
        await UnitOfWork.Courses.Add(course);
        return request.CourseDto;
    }
}