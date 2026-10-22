// using Application.DTOs.Response;
// using Domain.Entities;
// using Domain.Exceptions;
// using Domain.Interfaces.UnitOfWork;
// using MediatR;
// using Microsoft.Extensions.Logging;

// namespace Application.UseCases.Instructors.Commands;

// public class DeleteInstructorCommand : IRequest<BaseApiResponse<bool>>
// {
//     public required int Id { get; set; }
// }

// public class DeleteInstructorCommandHandler(IUnitOfWork UnitOfWork, ILogger<DeleteInstructorCommandHandler> Logger) : IRequestHandler<DeleteInstructorCommand, BaseApiResponse<bool>>
// {
//     public async Task<BaseApiResponse<bool>> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
//     {
//         Logger.LogInformation("Deleting {Instructor} With Id {Id}", nameof(Instructor), request.Id);
//         var model = await UnitOfWork.Instructors.GetById(request.Id);
//         if (model == null)
//             throw new NotFoundException(nameof(Instructor), request.Id);
//         await UnitOfWork.Instructors.Remove(model.Id);
//         var state = await UnitOfWork.Complete();
//         return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Instructor)} deleted successfully") : BaseApiResponse<bool>.Fail("Failed to delete instructor");
//     }
// }
