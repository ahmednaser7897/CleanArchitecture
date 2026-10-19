using Application.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.DTOs.Accounts;
using Application.InterfacesServices;
using Domain.Enums;

namespace Application.UseCases.Auth.Queries;

public class RegisterQuery(RegisterRequestDto dto, string role, int? officeId = null) : IRequest<BaseApiResponse<string>>
{
    public RegisterRequestDto Dto { get; set; } = dto;
    public string Role { get; set; } = role;
    public int? OfficeId { get; set; } = officeId;
}

public class RegisterQueryHandler(IAuthService Repository, IUnitOfWork UnitOfWork, ILogger<RegisterQueryHandler> Logger) : IRequestHandler<RegisterQuery, BaseApiResponse<string>>
{
    public async Task<BaseApiResponse<string>> Handle(RegisterQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Register in process");
        await UnitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var value = await Repository.Register(request.Dto, request.Role);
            if (value != null)
            {
                if (request.Role == UserRoles.Student)
                {
                    await CreateStudent(value);
                }
                else if (request.Role == UserRoles.Instructor)
                {
                    if (request.OfficeId == null)
                    {
                        throw new CusttomExption("OfficeId is required for instructor");
                    }
                    await CreateInstructor(value, request.OfficeId.Value);
                }
                await UnitOfWork.CommitTransactionAsync(cancellationToken);

                return BaseApiResponse<string>.Success("Registered successfully");
            }
            else
            {
                return BaseApiResponse<string>.Fail("Failed to register");
            }
        }
        catch
        {
            await UnitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
    async Task CreateStudent(string userId)
    {
        var user = new Student
        {
            UserId = userId,
        };
        await UnitOfWork.Students.Add(user);
        await UnitOfWork.Complete();
    }
    async Task CreateInstructor(string userId, int officeId)
    {
        var isExitsOffice = await UnitOfWork.Offices.GetById(officeId);
        if (isExitsOffice != null)
        {
            var user = new Instructor
            {
                UserId = userId,
                OfficeId = officeId,
            };
            await UnitOfWork.Instructors.Add(user);
            await UnitOfWork.Complete();
        }
        else
        {
            throw new CusttomExption("Office is not found");
        }
    }
}


