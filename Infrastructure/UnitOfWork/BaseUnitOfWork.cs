using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork;

public class BaseUnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private readonly AppDbContext _context;
    public IBaseRepository<Course, int> Courses { get; private set; }

    public IBaseRepository<Enrollment, int> Enrollments { get; private set; }

    public IBaseRepository<Instructor, int> Instructors { get; private set; }

    public IBaseRepository<Office, int> Offices { get; private set; }

    public IBaseRepository<Schedule, int> Schedules { get; private set; }

    public IBaseRepository<Section, int> Sections { get; private set; }

    public IBaseRepository<Student, int> Students { get; private set; }
    public IBaseRepository<RefreshToken, int> RefreshTokens { get; private set; }


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
        RefreshTokens = new BaseRepository<RefreshToken, int>(_context);
    }

    public Task<int> Complete()
    {
        return _context.SaveChangesAsync();
    }
    public async Task BeginTransactionAsync(
        CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {

            throw new CusttomExption(
                "A transaction is already active.");
        }


        _transaction = await _context.Database
            .BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(
        CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new CusttomExption(
                "No active transaction.");
        }

        try
        {
            await _transaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(
        CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
            return;

        try
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public virtual void Dispose()
    {
        // Prevent multiple disposes
        GC.SuppressFinalize(this);

        // Dispose the DbContext
        _transaction?.Dispose();
        _context.Dispose();
    }
}
