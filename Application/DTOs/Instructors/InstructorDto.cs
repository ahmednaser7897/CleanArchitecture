using Application.DTOs.Accounts;
using Application.DTOs.Offices;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.Instructors;

public class InstructorDto
{
    public required int Id { get; set; }
    public required UserDto User { get; set; }
    public required OfficeDto Office { get; set; }


    public static InstructorDto FromEntit(Instructor model)
    {
        return new InstructorDto
        {
            Id = model.Id,
            User = UserDto.FromEntit(model.User),
            Office = OfficeDto.FromEntit(model.Office),
        };
    }

    // public Instructor ToEntit()
    // {
    //     return new Instructor
    //     {
    //         Id = Id,
    //         OfficeId = Office.Id,
    //         UserId = User.Id,
    //     };
    // }
}
