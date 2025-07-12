using Hometask.OrderManager.Data.EF.Models;
using System.Linq.Expressions;

namespace Hometask.OrderManager.Data.EF.Repositories.Specifications;

public class ISpecification<T> where T : IEntity
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
}
