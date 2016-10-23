using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Arvato.IQ.Cor.Stores
{
    /// <summary>
    /// interface that exposes / represent basic store api
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IStore<TEntity, in TKey> : IDisposable
       where TEntity : class, new()
    {

        /// <summary>
        /// Get reference to the data store
        /// </summary>
        IDbStore DbStore { get; }

        /// <summary>
        /// Saving changes immediately to the data store ?
        /// </summary>
        bool AutoSaveChanges { get; set; }

        /// <summary>
        /// Dispose context when disposing the store ?
        /// </summary>
        bool DisposeContext { get; set; }

        /// <summary>
        /// Get all entities as IQueryable
        /// </summary>
        IQueryable<TEntity> All {
            get;
        }

        /// <summary>
        /// Find Entities
        /// </summary>
        /// <param name="startId"></param>
        /// <param name="take"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAsync(
            int page,
            int take,
            string orderBy);

        /// <summary>
        /// Find Entities
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="lastId"></param>
        /// <param name="take"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        Task<IEnumerable<TResult>> FindAsync<TResult>(
            int page,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            string orderBy,
            Expression<Func<TEntity, TResult>> selector);


        /// <summary>
        /// Get single or default entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find entity by id
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(TKey key);

        /// <summary>
        /// Get the total count of entities
        /// </summary>
        /// <returns></returns>
        Task<long> CountAsync();

        /// <summary>
        /// get the total count of entities based on custom criteria
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity"> the new entity to be added </param>
        /// <returns></returns>
        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Update entity 
        /// </summary>
        /// <param name="entity">the entity to be updated </param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete entity 
        /// </summary>
        /// <param name="entity">the entity to be deleted </param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
    }
}
