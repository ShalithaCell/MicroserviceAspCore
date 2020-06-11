using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Identity.Infrastructure.IServices.Repository
{
    /// <summary>
    /// Genaric interface for repository pattern
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get data from the database
        /// </summary>
        /// <param name="filter">filter linq query</param>
        /// <param name="orderBy">order parameter</param>
        /// <param name="includeProperties">foreign models</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Get data by using the ID attribute
        /// </summary>
        /// <param name="id">table identity ID</param>
        /// <returns></returns>
        TEntity GetByID(object id);

        /// <summary>
        /// Get data Count
        /// </summary>
        /// <param name="filter">filter linq query</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Get data as a collection
        /// </summary>
        /// <param name="filter">filter linq query</param>
        /// <param name="sortExpression">sorting expression</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="pageIndex">pageSize</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetCollection(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, bool>> sortExpression,
                                                    int pageSize, int pageIndex);

        /// <summary>
        /// Get data using SQL Query
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">Parameter set</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetWithRawSql(string query,
            params object[] parameters);

        /// <summary>
        /// Data Insert
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        
        /// <summary>
        /// Insert dataset
        /// </summary>
        /// <param name="entities">List of entities</param>
        void InsertBulk(List<TEntity> entities);

        /// <summary>
        /// Check data already exists if not add
        /// </summary>
        /// <param name="entity">entity</param>
        void AddOrUpdateSingle(TEntity entity);

        /// <summary>
        /// Check data already exixts, if not add (list)
        /// </summary>
        /// <param name="entities"></param>
        void AddOrUpdateBulk(List<TEntity> entities);

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="entityToUpdate">entityToUpdate</param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Remove data from entity
        /// </summary>
        /// <param name="entityToDelete">entityToDelete</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Remove data using expression
        /// </summary>
        /// <param name="filter">expression</param>
        void Delete(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Remove data using entity ID
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
    }
}
