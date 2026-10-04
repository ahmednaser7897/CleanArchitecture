using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces.Specification;

namespace Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
    //Basic Repository operations Async with not use specification pattern
    Task<TEntity?> GetById(TKey id);
    Task<List<TEntity>> GetAll();
    Task<bool> Add(TEntity model);
    Task<bool> Update(TEntity model);
    Task<bool> Remove(TKey id);
    //---------------------------------------------
    //Basic Repository operations Async with use specification pattern
    Task<TEntity?> GetByIdWithSpec(ISpecification<TEntity, TKey> spec, TKey id);
    Task<List<TEntity>> GetAllWithSpec(ISpecification<TEntity, TKey> spec);
    //Task<bool> AddWithSpec(ISpecification<TEntity, TKey> spec, TEntity model);
    //Task<bool> UpdateWithSpec(ISpecification<TEntity, TKey> spec, TEntity model);
    //Task<bool> RemoveWithSpec(ISpecification<TEntity, TKey> spec, TKey id);
    //---------------------------------------------
    Task<bool> SaveChangesAsync();
    //---------------------------------------------
}
