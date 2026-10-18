using Domain.Abstraction;

namespace Domain.Entities;

public class Student : IEntity<int>
{
    //student and section has many to many relationship
    // because one student can enroll in many sections
    // and one section can have many students
    public ICollection<Section> Sections { get; set; } = [];
    // student and ApplicationUser has one to one relationship
    // where student must has an ApplicationUser (Required)
    // where ApplicationUser may has an student (Optional)
    public required string UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public override string ToString()
    {
        return $"Name: {User.Name} | Id: {Id} ";
    }

}

