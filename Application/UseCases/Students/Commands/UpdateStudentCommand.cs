// using Application.DTOs.Response;
// using Application.DTOs.Students;
// using Domain.Entities;
// using Domain.Exceptions;
// using Domain.Interfaces.UnitOfWork;
// using MediatR;
// using Microsoft.Extensions.Logging;

// namespace Application.UseCases.Students.Commands;

// public class UpdateStudentCommand : IRequest<BaseApiResponse<bool>>
// {
//     public required StudentDto Dto { get; set; }
//     public required int Id { get; set; }
// }

// public class UpdateStudentCommandHandler(IUnitOfWork UnitOfWork, ILogger<UpdateStudentCommandHandler> Logger) : IRequestHandler<UpdateStudentCommand, BaseApiResponse<bool>>
// {
//     public async Task<BaseApiResponse<bool>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
//     {
//         Logger.LogInformation("Updating {Student} with Id {Id}", nameof(Student), request.Id);
//         var model = await UnitOfWork.Students.GetById(request.Id);
//         if (model == null)
//             throw new NotFoundException(nameof(Student), request.Id);
//         request.Dto.Id = model.Id;
//         await UnitOfWork.Students.Update(request.Dto.ToEntit());
//         var state = await UnitOfWork.Complete();
//         return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Student)} updated successfully") : BaseApiResponse<bool>.Fail("Failed to update student");
//     }
// }
