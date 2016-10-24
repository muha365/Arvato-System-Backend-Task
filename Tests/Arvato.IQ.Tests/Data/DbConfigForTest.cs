using Arvato.IQ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Tests.Data
{
    public class DbConfigForTest : DbStoreConfig
    {
        public DbConfigForTest() : base()
        {
            SetDatabaseInitializer<DbStore>(new DbStoreInitializer());
        }
    }
}
