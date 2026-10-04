namespace Domain.Entities;

public class Student : IEntity<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    //student and section has many to many relationship
    // because one student can enroll in many sections
    // and one section can have many students
    public ICollection<Section> Sections { get; set; } = [];
    public override string ToString()
    {
        return $"Name: {Name} | Id: {Id} ";
    }

}

