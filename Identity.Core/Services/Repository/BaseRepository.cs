using Identity.Infrastructure.DAL;
using Identity.Infrastructure.Extension;
using Identity.Infrastructure.IServices.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Identity.Core.Services.Repository
{
    class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal ApplicationDbContext context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter).AsNoTracking();
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty).AsNoTracking();
                }
            }


            if (orderBy != null)
            {
                return orderBy(query).AsNoTracking().ToList();
            }
            else
            {
                return query.AsNoTracking().ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            var entity = dbSet.Find(id);

            context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.Where(filter).Count();
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertBulk(List<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void AddOrUpdateSingle(TEntity entity)
        {
            dbSet.AddOrUpdate(entity);
        }

        public virtual void AddOrUpdateBulk(List<TEntity> entities)
        {
            foreach(TEntity entity in entities)
            {
                dbSet.AddOrUpdate(entity);
            }
            
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> filter)
        {
            dbSet.RemoveRange(dbSet.Where(filter));
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public IEnumerable<TEntity> GetCollection(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, bool>> sortExpression, int pageSize, int pageIndex)
        {
            return dbSet.Where(filter).OrderBy(sortExpression).Skip(pageSize * pageIndex).Take(pageSize).AsNoTracking();
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query,
            params object[] parameters)
        {
            return dbSet.FromSqlRaw(query, parameters).ToList();
        }
    }
}
