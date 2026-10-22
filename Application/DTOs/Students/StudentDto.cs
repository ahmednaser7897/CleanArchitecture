using Application.DTOs.Accounts;
using Domain.Entities;


namespace Application.DTOs.Students;

public class StudentDto
{
    public int Id { get; set; }
    public required UserDto User { get; set; }


    public static StudentDto FromEntit(Student model)
    {
        return new StudentDto
        {
            Id = model.Id,
            User = UserDto.FromEntit(model.User),
        };
    }

    // public Student ToEntit()
    // {
    //     return new Student
    //     {
    //         Id = Id,
    //         UserId = User.Id,
    //     };
    // }
}
