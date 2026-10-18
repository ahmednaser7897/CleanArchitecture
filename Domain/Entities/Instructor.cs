using Domain.Abstraction;
namespace Domain.Entities;
//instructor and office has one to one relationship
// where instructor must has an office (Required)
// where office may has an instructor (Optional)
public class Instructor : IEntity<int>
{
    public required string Name { get; set; }
    // instructor must has an office (Required)
    public required int OfficeId { get; set; }
    public Office Office { get; set; } = null!;
    //instructor and section has one to many relationship
    // where instructor may has many sections (Optional)
    // where section must has one instructor (Required)
    public ICollection<Section> Sections { get; set; } = [];
    // instructor and ApplicationUser has one to one relationship
    // where instructor must has an ApplicationUser (Required)
    // where ApplicationUser may has an instructor (Optional)
    public required string UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public override string ToString()
    {
        return $"Instructor Name: {Name} | Id: {Id}";
    }
}
