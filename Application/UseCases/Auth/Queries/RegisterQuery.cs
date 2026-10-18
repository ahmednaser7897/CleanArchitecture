using Application.DTOs.Response;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.DTOs.Accounts;
using Application.InterfacesServices;
using Domain.Enums;

namespace Application.UseCases.Auth.Queries;

public class RegisterQuery(RegisterRequestDto dto, string role) : IRequest<BaseApiResponse<string>>
{
    public RegisterRequestDto Dto { get; set; } = dto;
    public string Role { get; set; } = role;
}

public class RegisterQueryHandler(IAuthService Repository, IUnitOfWork UnitOfWork, ILogger<RegisterQueryHandler> Logger) : IRequestHandler<RegisterQuery, BaseApiResponse<string>>
{
    public async Task<BaseApiResponse<string>> Handle(RegisterQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Register in process");
        var value = await Repository.Register(request.Dto, request.Role);
        if (value != null)
        {
            if (request.Role == UserRoles.Student)
            {
                await CreateStudent(value);
            }
            return BaseApiResponse<string>.Success("Registered successfully");
        }
        else
        {
            return BaseApiResponse<string>.Fail("Failed to register");
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
}


