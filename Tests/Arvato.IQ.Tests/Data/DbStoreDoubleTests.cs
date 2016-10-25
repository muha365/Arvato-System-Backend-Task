using Arvato.IQ.Data;
using Arvato.IQ.Tests.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Arvato.IQ.Tests.Data
{
    [Collection("Double Tests")]
    public class DbStoreDoubleTests
    {
        public DbStoreDoubleTests()
        {

        }

        [Fact]
        [Trait("Double Testing","Stories")]
        public async void Should_Return_All_Stories()
        {
            var db = DbTestHelper.MockDbStore();
            var stories = await db.Set<Story>().ToListAsync();
            Assert.NotEmpty(stories);
            Assert.Equal<int>(stories.Count, 25);
        }
    }
}
