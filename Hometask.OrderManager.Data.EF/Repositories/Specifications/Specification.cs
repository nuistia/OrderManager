using Hometask.OrderManager.Data.EF.Models;
using System.Linq.Expressions;

namespace Hometask.OrderManager.Data.EF.Repositories.Specifications;

public class Specification<T> : ISpecification<T> where T : IEntity
{
    public Expression<Func<T, bool>> Criteria { get; init; } = _ => true;

    public List<Expression<Func<T, object>>> Includes { get; } = new();

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}
