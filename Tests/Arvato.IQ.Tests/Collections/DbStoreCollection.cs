using Arvato.IQ.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Arvato.IQ.Tests.Collections
{
    [CollectionDefinition("DbStoreCollection")]
    public class DbStoreCollection : ICollectionFixture<DbStoreFixture>
    {
        public DbStoreCollection()
        {
        }
    }
}
