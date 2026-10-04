namespace Domain.Entities;

public class Course : IEntity<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }

    // one course may has many sections (Optional)
    // but one section must has one course (Required)
    // so we will add CourseId to Section table
    public ICollection<Section> Sections { get; set; } = [];
    public override string ToString()
    {
        return $"Course Name: {Name} | Id: {Id} | Price {Price}";
    }
}

