// using Application.DTOs.Instructors;
// using Application.DTOs.Response;
// using Domain.Entities;
// using Domain.Exceptions;
// using Domain.Interfaces.UnitOfWork;
// using MediatR;
// using Microsoft.Extensions.Logging;

// namespace Application.UseCases.Instructors.Commands;

// public class UpdateInstructorCommand : IRequest<BaseApiResponse<bool>>
// {
//     public required InstructorDto Dto { get; set; }
//     public required int Id { get; set; }
// }

// public class UpdateInstructorCommandHandler(IUnitOfWork UnitOfWork, ILogger<UpdateInstructorCommandHandler> Logger) : IRequestHandler<UpdateInstructorCommand, BaseApiResponse<bool>>
// {
//     public async Task<BaseApiResponse<bool>> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
//     {
//         Logger.LogInformation("Updating {Instructor} with Id {Id}", nameof(Instructor), request.Id);
//         var model = await UnitOfWork.Instructors.GetById(request.Id);
//         if (model == null)
//             throw new NotFoundException(nameof(Instructor), request.Id);
//         request.Dto.Id = model.Id;
//         await UnitOfWork.Instructors.Update(request.Dto.ToEntit());
//         var state = await UnitOfWork.Complete();
//         return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Instructor)} updated successfully") : BaseApiResponse<bool>.Fail("Failed to update instructor");
//     }
// }
