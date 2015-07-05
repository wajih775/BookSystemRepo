using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataLayer.Interfaces
{
    /// <summary>
    /// Note that the return type of the two Get* methods is IList<T> rather than IQueryable<T>. 
    /// This means that the methods will be returning the actual already executed results from the queries rather than executable queries themselves. 
    /// Creating queries and return these back to the calling code would make the caller responsible for executing the LINQ-to-Entities queries and consequently use EF logic. 
    /// Besides, when using EF in an N-tier application the repository typically creates a new context and dispose it on every request meaning the calling code won’t have access to it and therefore the ability to cause the query to be executed. 
    /// Thus you should always keep your LINQ queries inside of the repository when using EF in a disconnected scenario such as in an N-tier application.
    /// Edit: Added an IQueryable method, to let the Business Layer execute customize queries
    /// </summary>
    ///

    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> GetAll();
        IList<T> GetAll(Expression<Func<T, bool>> where);
        T GetSingle(Expression<Func<T, bool>> where);
        int Add(params T[] items);
        int Update(params T[] items);
        int Remove(params T[] items);
        int RemoveAll(IList<T> itemList);

        /// <summary>
        /// Specific method for lazy loading by page index.
        /// </summary>
        /// <param name="where">Can be null, or data filter expression</param>
        /// <param name="orderBy">Required for lazy loading</param>
        /// <param name="pageIndex">Page index, starting from 0</param>
        /// <returns></returns>
        IList<T> GetLazyData(Expression<Func<T, bool>> where, Expression<Func<T, IComparable>> orderBy, int pageIndex);
        
    }
}
