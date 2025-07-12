using Hometask.OrderManager.Data.EF.Models;
using Hometask.OrderManager.Data.EF.Repositories.Specifications;

namespace Hometask.OrderManager.Data.EF.Repositories;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> GetAll();
    TEntity GetById(Guid id);
    List<TEntity> GetBySpec(Specification<TEntity> spec);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}
