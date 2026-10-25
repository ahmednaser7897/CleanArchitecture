using Application.DTOs.Response;
using Application.InterfacesServices;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Students;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
namespace Application.UseCases.Auth.Commands;

public class DeleteUserCommand(string userId, string role) : IRequest<BaseApiResponse<bool>>
{
    public string UserId { get; set; } = userId;
    public string Role { get; set; } = role;

}

public class DeleteUserCommandHandler(IAuthService Repository, IUnitOfWork UnitOfWork, UserManager<ApplicationUser> UserManager, ILogger<DeleteUserCommand> Logger) : IRequestHandler<DeleteUserCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("DeleteUser in process");
        await UnitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var userId = (await UserManager.FindByIdAsync(request.UserId) ?? throw new CusttomExption("User not found")).Id;
            if (request.Role == nameof(UserRoles.Student))
            {
                await DeleteStudent(userId);
            }
            else if (request.Role == nameof(UserRoles.Instructor))
            {
                //await DeleteInstructor(user);
            }
            var value = await Repository.DeleteUser(request.UserId);
            await UnitOfWork.CommitTransactionAsync(cancellationToken);
            return value ? BaseApiResponse<bool>.Success(true) : BaseApiResponse<bool>.Fail("Failed to delete user");
        }
        catch
        {
            await UnitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }

    }
    public async Task DeleteStudent(string UserId)
    {
        Logger.LogInformation("Delete Student");
        var Students = await UnitOfWork.Students.GetAllWithSpec(new GetStudentWuthUserSpec(UserId));
        if (Students.Count == 0)
        {
            throw new CusttomExption("Student not found");
        }
        var Student = Students[0];
        var value = await UnitOfWork.Students.Remove(Student.Id);
        if (!value)
        {
            throw new CusttomExption("Failed to delete student");
        }
    }
    // public async Task DeleteInstructor(ApplicationUser user)
    // {
    //     var Instructor = await UnitOfWork.Instructors.GetById(user!.Instructor?.Id ?? -1) ?? throw new CusttomExption("Instructor not found");
    //     var value = await UnitOfWork.Instructors.Remove(Instructor.Id);
    //     if (!value)
    //     {
    //         throw new CusttomExption("Failed to delete instructor");
    //     }
    // }

}


