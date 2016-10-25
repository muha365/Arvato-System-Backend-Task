using Arvato.IQ.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Data
{
    /// <summary>
    /// The database DbContext that implements IDbStore and works as unit of work for  
    /// </summary>
    public partial class DbStore : DbContext, IDbStore
    {
        /// <summary>
        /// the default constructor
        /// </summary>
        public DbStore() : this("name=DbStoreConnection")
        {
        }

        /// <summary>
        /// constructor accept connection string
        /// </summary>
        /// <param name="nameOrConnectionString">the database connection string </param>
        public DbStore(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Building up Database Configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(typeof(DbStore).Assembly);
        }

        /// <summary>
        /// Dispose DbStore
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {

#if DEBUG
            Debug.WriteLine("DbStore : Disposing .... ");
#endif
            base.Dispose(disposing);
        }

        /// <summary>
        /// Save changes Asynchronous
        /// </summary>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync()
        {
            // any additional processing before saving will occure here  
            return base.SaveChangesAsync();
        }

        /// <summary>
        /// Create new instance of DbStore
        /// </summary>
        /// <returns></returns>
        public static DbStore Create()
        {
            var db = new DbStore();
            db.Database.Initialize(true);
            return db;
        }

        /// <summary>
        /// Set entity as modified 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modifiedObject"></param>
        public virtual void SetModified<T>(T modifiedObject) where T : class, new()
        {
            Entry(modifiedObject).State = EntityState.Modified;
        } 
    }  
} 