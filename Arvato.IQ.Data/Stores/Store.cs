using Arvato.IQ.Core;
using Arvato.IQ.Core.Stores;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using Arvato.IQ.Core.Helpers;

namespace Arvato.IQ.Data.Stores
{

    /// <summary>
    /// abstract class for basic store implementation
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Store<TEntity, TKey> : IStore<TEntity, TKey>
     where TEntity : class, new()
     where TKey : IEquatable<TKey>
    {

        EntityStore<TEntity> store;
        bool disposed = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public Store(DbStore context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            DbStore = context;
            store = new EntityStore<TEntity>(context);
            AutoSaveChanges = true;
        }

        /// <summary>
        /// Data Store 
        /// </summary>
        public virtual IDbStore DbStore { get; private set; }

        /// <summary>
        /// Save changes immediately  
        /// </summary>
        public virtual bool AutoSaveChanges { get; set; }

        /// <summary>
        /// Dispose context automatically when dispose store
        /// </summary>
        public virtual bool DisposeContext { get; set; }

        /// <summary>
        /// Get all entities As IQueryable
        /// </summary>
        public virtual IQueryable<TEntity> All {
            get {
                return store.EntitySet;
            }
        }

        /// <summary>
        /// Find entities asynchronously
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAsync(int page, int take, string orderBy)
        {
            return await All
                .OrderBy(orderBy).Page(page, take).ToListAsync();
        }

        /// <summary>
        /// find entities asynchronously
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TResult>> FindAsync<TResult>(int page, int take, Expression<Func<TEntity, bool>> predicate, string orderBy, Expression<Func<TEntity, TResult>> selector)
        {
            ThrowIfDisposed();
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (string.IsNullOrEmpty(orderBy))
            {
                throw new ArgumentException("Argument {0} shouldnot be null or empty string", nameof(orderBy));
            }
            return await All
                .Where(predicate)
                .OrderBy(orderBy).Page(page, take)
                .Select(selector)
                .ToListAsync();
        }

        /// <summary>
        /// Get single or default entity asynchronously
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ThrowIfDisposed();
            IQueryable<TEntity> query = All;
            if (predicate != null)
            {
                return await query.SingleOrDefaultAsync(predicate);
            }
            return await query.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Get total count of entities asynchronously
        /// </summary>
        /// <returns></returns>
        public virtual async Task<long> CountAsync()
        {
            ThrowIfDisposed();
            IQueryable<TEntity> query = All;
            return await query.LongCountAsync();
        }

        /// <summary>
        /// get entities count based on custom criteria
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ThrowIfDisposed();
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            IQueryable<TEntity> query = All.Where(predicate);
            return await query.LongCountAsync();
        }

        /// <summary>
        /// Create new entity asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task CreateAsync(TEntity entity)
        {
            ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            store.Create(entity);

            if (AutoSaveChanges)
                await SaveChanges().WithCurrentCulture();
        }

        /// <summary>
        /// Delete entity asychronously 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            store.Delete(entity);
            await SaveChanges().WithCurrentCulture();
        }

        /// <summary>
        /// Get entity by Id
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindByIdAsync(TKey key)
        {
            ThrowIfDisposed();
            var entity = await store.FindByIdAsync(key).WithCurrentCulture();
            return entity;
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(TEntity));
            }
            store.Update(entity);
            if (AutoSaveChanges)
                await DbStore.SaveChangesAsync().WithCurrentCulture();
            else
                await Task.FromResult(0);
        }

        // Only call save changes if AutoSaveChanges is true
        private async Task SaveChanges()
        {
            if (AutoSaveChanges)
            {
                await DbStore.SaveChangesAsync().WithCurrentCulture();
            }
        }

        /// <summary>
        /// Check if store is disposing
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        /// Dispose store
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Disposing store resources
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed && DisposeContext)
            {
                DbStore.Dispose();
                disposed = true;
            }
        }

    }

}
