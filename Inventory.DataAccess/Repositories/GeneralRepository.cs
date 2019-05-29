using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Inventory.DataAccess.Repositories
{
    public abstract class GeneralRepository<T, M> where T : class where M : class
    {
        protected DbContext Context = new Context.Context();
        protected DbSet<T> DbSet;

        public GeneralRepository()
        {
            DbSet = Context.Set<T>();
            MapperConfiguration.AutoMapperConfiguration.Configure();
        }

        public virtual IEnumerable<M> Get()
        {
            return AutoMapper.Mapper.Map<IEnumerable<M>>(DbSet);
        }

        public virtual void Insert(M entity)
        {
            T e = AutoMapper.Mapper.Map<T>(entity);
            DbSet.Add(e);
        }

        public virtual void Delete(M entity)
        {
            T e = AutoMapper.Mapper.Map<T>(entity);
            DbSet.Remove(e);
        }

        public virtual IQueryable<M> Filter(Expression<Func<M, bool>> expresion)
        {
            var exp = AutoMapper.Mapper.Map<Expression<Func<T, bool>>>(expresion);
            var result = DbSet.Where(exp).ToList();
            return AutoMapper.Mapper.Map<IQueryable<M>>(result);
        }

        public virtual M GetById(int id)
        {
            var result = DbSet.Find(id);
            return AutoMapper.Mapper.Map<M>(result);
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
