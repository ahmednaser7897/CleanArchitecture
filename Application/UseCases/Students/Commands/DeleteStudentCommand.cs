// using Application.DTOs.Response;
// using Domain.Entities;
// using Domain.Exceptions;
// using Domain.Interfaces.UnitOfWork;
// using MediatR;
// using Microsoft.Extensions.Logging;

// namespace Application.UseCases.Students.Commands;

// public class DeleteStudentCommand : IRequest<BaseApiResponse<bool>>
// {
//     public required int Id { get; set; }
// }

// public class DeleteStudentCommandHandler(IUnitOfWork UnitOfWork, ILogger<DeleteStudentCommandHandler> Logger) : IRequestHandler<DeleteStudentCommand, BaseApiResponse<bool>>
// {
//     public async Task<BaseApiResponse<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
//     {
//         Logger.LogInformation("Deleting {Student} With Id {Id}", nameof(Student), request.Id);
//         var model = await UnitOfWork.Students.GetById(request.Id);
//         if (model == null)
//             throw new NotFoundException(nameof(Student), request.Id);
//         await UnitOfWork.Students.Remove(model.Id);
//         var state = await UnitOfWork.Complete();
//         return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Student)} deleted successfully") : BaseApiResponse<bool>.Fail("Failed to delete student");
//     }
// }
