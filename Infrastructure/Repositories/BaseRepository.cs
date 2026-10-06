using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Specification;
using Infrastructure.Contexts;
using Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<TEntity, TKey>(AppDbContext context)
: IBaseRepository<TEntity, TKey>
where TEntity : class, IEntity<TKey>
{
    protected readonly AppDbContext _context = context;

    public async Task<bool> Add(TEntity model)
    {
        await _context.Set<TEntity>().AddAsync(model);
        return true;
    }

    public async Task<List<TEntity>> GetAll()
    {
        var value = await _context.Set<TEntity>().ToListAsync();
        return value;
    }

    public async Task<TEntity?> GetById(TKey id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<bool> Remove(TKey id)
    {
        var model = await GetById(id);
        if (model != null)
        {
            _context.Set<TEntity>().Remove(model);
            return true;
        }
        return false;
    }

    public async Task<bool> Update(TEntity model)
    {
        _context.Set<TEntity>().Update(model);
        return true;
    }

    //=======================================================

    public async Task<List<TEntity>> GetAllWithSpec(
        ISpecification<TEntity, TKey> spec)
    {
        var baseQuery = _context.Set<TEntity>()
                                .AsNoTracking();
        return await SpecificationEvaluator.GenerateQuery<TEntity, TKey>(
            baseQuery,
            spec
        ).ToListAsync();

    }


    public async Task<TEntity?> GetByIdWithSpec(
        ISpecification<TEntity, TKey> spec,
        TKey id)
    {
        return (await GetAllWithSpec(spec)).FirstOrDefault(x => (x.Id).Equals(id));
    }
    //=======================================================
    public async Task<bool> SaveChangesAsync()
    {
        var result = await _context.SaveChangesAsync() > 0;
        return result;
    }
}

