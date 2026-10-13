namespace Infrastructure.Seeders;

public static class UserRoles
{
    public static readonly List<string> Roles = [
        Admin,
        Student,
        Instructor
    ];
    public const string Admin = "Admin";
    public const string Student = "Student";
    public const string Instructor = "Instructor";
}