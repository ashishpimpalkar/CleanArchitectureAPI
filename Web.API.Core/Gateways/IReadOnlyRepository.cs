using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace Web.API.Core.Gateways
{
    public interface IReadOnlyRepository<T>
    {
        /// <summary>
        /// methods to retun the all data realted to T
        /// </summary>
        /// <returns></returns>

        IQueryable<T> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T,bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate,int pageNumber,int pageSize);

        

    }
}
