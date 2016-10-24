using Arvato.IQ.Data;
using Arvato.IQ.Tests.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Tests.Fixtures
{
    public class DbStoreFixture : IDisposable
    {
        private Lazy<DbStore> database = new Lazy<DbStore>(() => {
            DbConfiguration.SetConfiguration(new DbConfigForTest());
            var db = new DbStore("DbStoreConnection-test");
            db.Database.Initialize(true);
            return db;
        });

        public DbStoreFixture()
        {
        }

        public DbStore Database {
            get {
                return database.Value;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Database.Dispose();
            }
        }
    }

}
