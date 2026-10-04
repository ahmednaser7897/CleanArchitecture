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
        return await SaveChangesAsync();
    }


    public async Task<List<TEntity>> GetAll()
    {
        return await _context.Set<TEntity>().ToListAsync();
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
            return await SaveChangesAsync();
        }
        return false;
    }



    public async Task<bool> Update(TEntity model)
    {
        _context.Set<TEntity>().Update(model);
        return await SaveChangesAsync();
    }

    //=======================================================
    //=======================================================

    // public async Task<bool> AddWithSpec(
    //     ISpecification<TEntity, TKey> spec,
    //     TEntity model)
    // {
    //     // Specifications are usually used for querying.
    //     // For Add, we just add the entity and save.
    //     await _context.Set<TEntity>().AddAsync(model);

    //     return await SaveChangesAsync();
    // }


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


    // public async Task<bool> RemoveWithSpec(
    //     ISpecification<TEntity, TKey> spec,
    //     TKey id)
    // {
    //     // Get entity
    //     var data = await GetAllWithSpec(spec);
    //     var model = data.FirstOrDefault(x => x.Id.Equals(id));

    //     if (model == null)
    //         return false;

    //     _context.Set<TEntity>().Remove(model);

    //     return await SaveChangesAsync();
    // }


    // public async Task<bool> UpdateWithSpec(
    //     ISpecification<TEntity, TKey> spec,
    //     TEntity model)
    // {
    //     // Get entity
    //     var data = await GetAllWithSpec(spec);
    //     var existingEntity = data.FirstOrDefault(x => x.Id.Equals(model.Id));

    //     if (existingEntity == null)
    //         return false;

    //     _context.Set<TEntity>().Update(model);

    //     return await SaveChangesAsync();
    // }

    //=======================================================
    public async Task<bool> SaveChangesAsync()
    {
        var result = await _context.SaveChangesAsync() > 0;
        return result;
    }
}

