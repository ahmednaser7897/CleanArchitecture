// using Application.DTOs.Enrollments;
// using Application.DTOs.Response;
// using Domain.Entities;
// using Domain.Exceptions;
// using Domain.Interfaces.UnitOfWork;
// using MediatR;
// using Microsoft.Extensions.Logging;

// namespace Application.UseCases.Enrollments.Commands;

// public class UpdateEnrollmentCommand : IRequest<BaseApiResponse<bool>>
// {
//     public required EnrollmentDto Dto { get; set; }
//     public required int Id { get; set; }
// }

// public class UpdateEnrollmentCommandHandler(IUnitOfWork UnitOfWork, ILogger<UpdateEnrollmentCommandHandler> Logger) : IRequestHandler<UpdateEnrollmentCommand, BaseApiResponse<bool>>
// {
//     public async Task<BaseApiResponse<bool>> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
//     {
//         Logger.LogInformation("Updating {Enrollment} with Id {Id}", nameof(Enrollment), request.Id);
//         var model = await UnitOfWork.Enrollments.GetById(request.Id);
//         if (model == null)
//             throw new NotFoundException(nameof(Enrollment), request.Id);
//         request.Dto.Id = model.Id;
//         await UnitOfWork.Enrollments.Update(request.Dto.ToEntit());
//         var state = await UnitOfWork.Complete();
//         return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Enrollment)} updated successfully") : BaseApiResponse<bool>.Fail("Failed to update enrollment");
//     }
// }
