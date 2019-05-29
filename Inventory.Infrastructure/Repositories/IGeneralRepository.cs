using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Inventory.Infrastructure.Repositories
{
    public interface IGeneralRepository<T> where T : class
    {
        IEnumerable<T> Get();

        void Insert(T entity);

        void Delete(T entity);

        IQueryable<T> Filter(Expression<Func<T, bool>> expresion);

        T GetById(int id);

        void SaveChanges();
    }
}
