using Arvato.IQ.Data;
using Arvato.IQ.Tests.Data.Helpers;
using Arvato.IQ.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Xunit;

namespace Arvato.IQ.Tests.Data
{
    [Collection("DbStoreCollection")]
    public class DbStoreIntegrationTests
    {
        public DbStoreFixture DbFixture { get; private set; }

        public DbStoreIntegrationTests(DbStoreFixture db)
        {
            DbFixture = db;
        }

        [Fact]
        [Trait("Integration_Tests", "Stories Schema")]
        public void Should_Verify_Stories_Table_Schema()
        {
            string[] fields = { "StoryId", "Title", "Description", "PublishedAt" };
            string[] indexes = { "TitleIX"};
            DbTestHelper.VerifyTableSchema(DbFixture.Database, "Stories", fields);
            
        }

       
    }
}
