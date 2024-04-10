using System.Linq.Expressions;
using Core.Entities;

namespace Core.DataAccess
{
    public interface IRepositoryBase<TEntity>
       where TEntity : class, IEntity
    {
    }
}
