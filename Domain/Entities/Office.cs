namespace Domain.Entities;

public class Office : IEntity<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    //instructor and office has one to one relationship
    // where instructor must has an office (Required)
    // where office may has an instructor (Optional)
    public int? InstructorId { get; set; }
    public Instructor? Instructor { get; set; }
    public override string ToString()
    {
        return $"Office Name: {Name} | Id: {Id} OfficeLocation: {Location}";
    }
}
