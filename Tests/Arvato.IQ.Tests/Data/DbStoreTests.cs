using Arvato.IQ.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Arvato.IQ.Tests.Data
{
    [Collection("DbStoreCollection")]
    public class DbStoreTests
    {
        public DbStoreFixture DbFixture { get; private set; }

        public DbStoreTests(DbStoreFixture db)
        {
            DbFixture = db;
        }

        [Fact]
        [Trait("Database_Schema", "")]
        public void Should_Verify_Stories_Table_Schema()
        {
            string[] fields = { "StoryId", "Title", "Description", "PublishedAt" };
            string[] indexes = { "TitleIX"};
            DbTestHelper.VerifyTableSchema(DbFixture.Database, "Stories", fields);
            
        }
    }
}
