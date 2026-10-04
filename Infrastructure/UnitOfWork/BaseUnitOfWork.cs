using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Contexts;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;

public class BaseUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IBaseRepository<Course, int> Courses { get; private set; }

    public IBaseRepository<Enrollment, int> Enrollments { get; private set; }

    public IBaseRepository<Instructor, int> Instructors { get; private set; }

    public IBaseRepository<Office, int> Offices { get; private set; }

    public IBaseRepository<Schedule, int> Schedules { get; private set; }

    public IBaseRepository<Section, int> Sections { get; private set; }

    public IBaseRepository<Student, int> Students { get; private set; }


    public BaseUnitOfWork(AppDbContext context)
    {
        _context = context;
        Courses = new BaseRepository<Course, int>(_context);
        Enrollments = new BaseRepository<Enrollment, int>(_context);
        Instructors = new BaseRepository<Instructor, int>(_context);
        Offices = new BaseRepository<Office, int>(_context);
        Schedules = new BaseRepository<Schedule, int>(_context);
        Sections = new BaseRepository<Section, int>(_context);
        Students = new BaseRepository<Student, int>(_context);
    }



    public Task<int> Complete()
    {
        return _context.SaveChangesAsync();
    }

    public virtual void Dispose()
    {
        // Prevent multiple disposes
        GC.SuppressFinalize(this);

        // Dispose the DbContext
        _context.Dispose();
    }
}
