using Arvato.IQ.Core.Helpers;
using Arvato.IQ.Data;
using Arvato.IQ.Data.Stores;
using Arvato.IQ.Tests.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Arvato.IQ.Tests.Data.Stores
{
    public class StoryStoreTests
    {

        [Fact]
        [Trait("Story Store", "Context")]
        public void Should_Throw_ArgumentNullException_When_Context_Is_Null()
        {
            // Arrange
            DbStore context = null;
            StoryStore<Story> store;
            //Act & Assert ;)
            var ex = Assert.Throws<ArgumentNullException>(() => store = new StoryStore<Story>(context));
            Assert.Equal("context", ex.ParamName);

        }

        [Theory]
        [Trait("Story Store", " FindAsync")]
        [InlineData(1, 5, "StoryId Asc")]
        public async void Should_Find_Stories(int page, int take, string orderBy)
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);

            // Act
            var stories = await store.FindAsync(page, take, orderBy);

            //Assert
            Assert.NotNull(stories);
            Assert.NotEmpty(stories);
            Assert.Equal<int>(5, stories.Count());
        }

        [Fact]
        [Trait("Story Store", " FindAsync")]
        public async void Should_Throw_ArgumentException_Find_Stories_With_Null_OrderBy()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);


            // Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await store.FindAsync(1, 5, null));
            //Assert
            Assert.Equal<string>("orderBy", ex.ParamName);
        }

        [Fact]
        [Trait("Story Store", " FindAsync")]
        public async void Should_Find_Stories_And_Projecting_Successfully()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);

            // Act
            var stories = await store.FindAsync(1, 5, (Story s) => s.StoryId > 10, "StoryId Asc", (Story s) => new { Id = s.StoryId, Title = s.Title });

            //Assert
            Assert.NotNull(stories);
            Assert.NotEmpty(stories);
            Assert.True(stories.First().Id == 11);
        }

        [Fact]
        [Trait("Story Store", " FindAsync")]
        public async void Should_Throw_ArgumentNullException_Find_Stories_And_Projecting_With_Null_Predication()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);


            // Act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await store.FindAsync(1, 5, null, "StoryId Asc", (Story s) => new { Id = s.StoryId, Title = s.Title }));
            //Assert
            Assert.Equal<string>("predicate", ex.ParamName);
        }

        [Fact]
        [Trait("Story Store", " FindAsync")]
        public async void Should_Throw_ArgumentException_Find_Stories_And_Projecting_With_Null_OrderBy()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);


            // Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await store.FindAsync(1, 5, t => t.StoryId > 5, null, (Story s) => new { Id = s.StoryId, Title = s.Title }));
            //Assert
            Assert.Equal<string>("orderBy", ex.ParamName);
        }

        [Theory]
        [Trait("Story Store", "FindByIdAsync")]
        [InlineData(20)]
        public async void Should_Find_Story_By_Id(long id)
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);

            // Act
            var story = await store.FindByIdAsync(id);

            //Assert
            Assert.NotNull(story);
            Assert.Equal<long>(20, story.StoryId);
        }

        [Fact]
        [Trait("Story Store", "FirstOrDefaultAsync")]
        public async void Should_Find_FirstOrDefualt_Story()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);

            // Act
            var story = await store.FirstOrDefaultAsync(t => t.StoryId == 19);

            //Assert
            Assert.NotNull(story);
            Assert.Equal<long>(19, story.StoryId);
        }

        [Fact]
        [Trait("Story Store", "FirstOrDefaultAsync")]
        public async void Should_Throw_ArgumentNullException_FirstOrDefualt_Story_Null_Predicate()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);

            // Act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await store.FirstOrDefaultAsync(null));
            //Assert
            Assert.Equal<string>("predicate", ex.ParamName);
        }


        [Fact]
        [Trait("Story Store", "SingleOrDefaultAsync")]
        public async void Should_Find_SingleOrDefualt_Story()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);

            // Act
            var story = await store.SingleOrDefaultAsync(t => t.StoryId == 19);

            //Assert
            Assert.NotNull(story);
            Assert.Equal<long>(19, story.StoryId);
        }

        [Fact]
        [Trait("Story Store", "SingleOrDefaultAsync")]
        public async void Should_Throw_ArgumentNullException_SingleOrDefualt_Story_Null_Predicate()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);

            // Act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await store.SingleOrDefaultAsync(null));
            //Assert
            Assert.Equal<string>("predicate", ex.ParamName);
        }


        [Fact]
        [Trait("Story Store", " CreateAsync")]
        public async void Should_Create_Story_Successfully()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);
            var story = new Story() { StoryId = 100, Title = "Story 100", Description = "testing", PublishedAt = DateTime.UtcNow };

            // Act
            await store.CreateAsync(story);
            var newStory = store.FindByIdAsync(100);
            //Assert
            Assert.NotNull(newStory);
            Assert.Equal<long>(100, story.StoryId);
        }

        [Fact]
        [Trait("Story Store", " CreateAsync")]
        public async void Should_Throw_ArgumentNullException_Creating_Null_Story()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);
            Story story = null;

            // Act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await store.CreateAsync(story));
            Assert.Equal<string>("entity", ex.ParamName);


        }

        [Fact]
        [Trait("Story Store", " UpdateAsync")]
        public async void Should_Update_Story_Successfully()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);
            var story = await store.FirstOrDefaultAsync(t => t.StoryId == 5);

            // Act Assert
            Assert.NotNull(store);
            story.Title = "updated story";
            await store.UpdateAsync(story);

            var updatedStory = store.SingleOrDefaultAsync(t => t.StoryId == story.StoryId && t.Title.ToLower() == story.Title.ToLower());
            Assert.NotNull(updatedStory);
        }

        [Fact]
        [Trait("Story Store", " UpdateAsync")]
        public async void Should_Throw_ArgumentNullException_Updating_Null_Story()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);
            Story story = null;

            // Act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await store.UpdateAsync(story));
            Assert.Equal<string>("entity", ex.ParamName);
        }

        [Fact]
        [Trait("Story Store", "DeleteAsync")]
        public async void Should_Delete_Story_Successfully()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);
            var story = await store.FirstOrDefaultAsync(t => t.StoryId == 8);

            // Act Assert
            Assert.NotNull(store);
            await store.DeleteAsync(story);
            var deletedStory = await store.FindByIdAsync(8);
            Assert.Null(deletedStory);
        }

        [Fact]
        [Trait("Story Store", "DeleteAsync")]
        public async void Should_Throw_ArgumentNullException_Deleting_Null_Story()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);
            Story story = null;

            // Act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await store.DeleteAsync(story));
            //Assert
            Assert.Equal<string>("entity", ex.ParamName);
        }


        [Fact]
        [Trait("Story Store", " CountAsync")]
        public async void Should_Count_Stories_Successfully()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);
            var storiesCount = db.Set<Story>().LongCount();
            var countViaStore = await store.CountAsync();
            // Act Assert
            Assert.Equal<long>(storiesCount, countViaStore);
        }

        [Fact]
        [Trait("Story Store", " CountAsync")]
        public async void Should_Count_Project_Stories_Successfully()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);
            var storiesCount = db.Set<Story>().LongCount(s => s.StoryId > 20);
            var countViaStore = await store.CountAsync(s => s.StoryId > 20);
            // Act Assert
            Assert.Equal<long>(storiesCount, countViaStore);
        }

        [Fact]
        [Trait("Story Store", " CountAsync")]
        public async void Should_Throw_ArgumentNullException_Count_Project_Stories_Null_Predicate()
        {
            // Arrange
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);  

            //Act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await store.CountAsync(null));

            //Assert
            Assert.Equal<string>("predicate", ex.ParamName);
        }

        [Fact]
        [Trait("Story Store", "Dispose")]
        public void Should_Dispose_Context_On_DisposeStore()
        {
            var context = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(context);
            store.DisposeContext = true;

            store.Dispose();
            Assert.Throws<ObjectDisposedException>(() => AsyncHelper.RunSync(() => store.FindByIdAsync(4)));
        }

    }
}
