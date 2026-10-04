namespace Domain.Entities;
// Student and section has many to many relationship
// because one student can enroll in many sections
// and one section can have many students
// so we need a join table (Entity) to store the relationship
// and this join table will have the foreign keys of both tables
public class Enrollment : IEntity<int>
{
    public int Id { get; set; }
    public required int SectionId { get; set; }
    public required int StudentId { get; set; }

    public Section Section { get; set; } = null!;
    public Student Student { get; set; } = null!;

    public override string ToString()
    {
        return $"Section: {Section.Name} , Student: {Student.Name}";
    }
}

