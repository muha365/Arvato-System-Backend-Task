using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Data
{
    /// <summary>
    /// DbStore Configuration 
    /// </summary>
    public class DbStoreConfig : DbConfiguration
    {
        /// <summary>
        /// the default constructor
        /// </summary>
        public DbStoreConfig()
        {
            SetDatabaseInitializer<DbStore>(null);
            SetManifestTokenResolver(new DefaultManifestTokenResolver());
        }

        internal class DefaultManifestTokenResolver : IManifestTokenResolver
        {
            private readonly IManifestTokenResolver _defaultResolver = new System.Data.Entity.Infrastructure.DefaultManifestTokenResolver();

            public string ResolveManifestToken(DbConnection connection)
            {
                var sqlConn = connection as SqlConnection;
                if (sqlConn != null && sqlConn.DataSource == @".")
                {
                    return "2012";
                }
                else
                {
                    return _defaultResolver.ResolveManifestToken(connection);
                }
            }
        }

    }
}
