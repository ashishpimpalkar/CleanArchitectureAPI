using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace Web.API.Core.Gateways
{
    public interface IGenericRepository<T>:IReadOnlyRepository<T> where T :class
    {
        /// <summary>
        /// Method used for adding entity
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Method used for updating entity
        /// </summary>
        /// <param name="entity"></param>
        void Edit(T entity);

        /// <summary>
        /// Method used for deleting entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        
        /// <summary>
        /// Method used for saving entity
        /// </summary>
        void Save();

        /// <summary>
        /// List of parameter pass to the storedprocedure
        /// </summary>
        /// <param name="storedProcWithParams"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteStoredProcedure(string storedProcWithParams);

        IQueryable<T> FindBy(Expression<Func<T,bool>> predicate, string includeProp);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, List<string> includeProp);

        IQueryable<T> GetAll(List<string> includeProps);
        IQueryable<T> GetAll(int pageNumber, int pageSize);

        string ToSQL<T>(IQueryable<T> query) where T : class;



    }
}
