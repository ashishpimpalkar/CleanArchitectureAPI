using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Web.API.Core;
using System.Data.SqlClient;
using System.Data;
using Web.API.Core.Gateways;
using Web.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> ,IDisposable where T:class
    {
        /// <summary>
        /// 
        /// Lazy Intialization of Context
        /// </summary>
        Lazy<CleanContext> dbContext = null;

        /// <summary>
        /// Constructor used for initializing instance of context
        /// </summary>
        public GenericRepository(Lazy<CleanContext> ctx)
        {
            dbContext = ctx;
        }

        public void Add(T entity)
        {
            dbContext.Value.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Value.Set<T>().Remove(entity);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Edit(T entity)
        {
            dbContext.Value.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public IEnumerable<T> ExecuteStoredProcedure(string storedProcWithParams)
        {
            return dbContext.Value.Set<T>().FromSql(storedProcWithParams);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string includeProp)
        {
            IQueryable<T> query = dbContext.Value.Set<T>().Include(includeProp).Where(predicate);
            return query;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, List<string> includeProp)
        {
            IQueryable<T> query = dbContext.Value.Set<T>();
            includeProp.ForEach(st => query = query.Include(st));
            return query.Where(predicate);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = dbContext.Value.Set<T>().Where(predicate);
            return query;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        {
            IQueryable<T> query = dbContext.Value.Set<T>()
                .Where(predicate)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);
            return query;
                
        }

        public IQueryable<T> GetAll(List<string> includeProps)
        {
            var  query = dbContext.Value.Set<T>();
            return query;
        }

        public IQueryable<T> GetAll(int pageNumber, int pageSize)
        {
            IQueryable<T> query = dbContext.Value.Set<T>()
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);

            return query;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = dbContext.Value.Set<T>();
            return query;
        }

        public void Save()
        {
            dbContext.Value.SaveChanges();
        }

        public string ToSQL<T1>(IQueryable<T1> query) where T1 : class
        {
            throw new NotImplementedException();
        }
    }
}
