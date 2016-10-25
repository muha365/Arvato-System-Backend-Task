using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Core.Helpers
{
    /// <summary>
    /// Linq Extensions
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Get page from Queryable data using linq to sql
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">the data source</param>
        /// <param name="page">page index</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Get page from Enumerable data using linq 
        /// </summary>
        /// <typeparam name="TSource">the data source</typeparam>
        /// <param name="source"></param>
        /// <param name="page">page index</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>

        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
