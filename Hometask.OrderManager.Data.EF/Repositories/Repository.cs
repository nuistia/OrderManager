using Hometask.OrderManager.Data.EF.Models;
using Hometask.OrderManager.Data.EF.Repositories.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Hometask.OrderManager.Data.EF.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected CHIITDbContext Context { get; init; }
    private readonly DbSet<TEntity> _entities;

    public Repository(CHIITDbContext context)
    {
        Context = context ?? throw new ArgumentException();
        _entities = Context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        _entities.Add(entity);
        Context.SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        _entities.Remove(entity);
        Context.SaveChanges();
    }

    public List<TEntity> GetBySpec(Specification<TEntity> spec)
    {
        IQueryable<TEntity> query = _entities;

        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        foreach (var include in spec.Includes)
            query = query.Include(include);

        return query.ToList();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _entities.AsQueryable();
    }

    public TEntity GetById(Guid id)
    {
        return _entities.Find(id);
    }

    public void Update(TEntity entity)
    {
        _entities.Update(entity);
        Context.SaveChanges();
    }
}
