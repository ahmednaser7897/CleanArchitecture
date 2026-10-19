using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<Course, int> Courses { get; }
    public IBaseRepository<Enrollment, int> Enrollments { get; }
    public IBaseRepository<Instructor, int> Instructors { get; }
    public IBaseRepository<Office, int> Offices { get; }
    public IBaseRepository<Schedule, int> Schedules { get; }
    public IBaseRepository<Section, int> Sections { get; }
    public IBaseRepository<Student, int> Students { get; }
    Task BeginTransactionAsync(
       CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(
        CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(
        CancellationToken cancellationToken = default);

    public Task<int> Complete();
}
