using Arvato.IQ.Api.Controllers;
using Arvato.IQ.Api.Models;
using Arvato.IQ.Data;
using Arvato.IQ.Data.Stores;
using Arvato.IQ.Tests.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using Xunit;

namespace Arvato.IQ.Tests.Api
{
    public class StoriesControllerTests
    {

        [Fact]
        public async void ShouldReturnPagedStoriesResources()
        {
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);
            var controller = new StoriesController(store);
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/stories")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary {{ "controller", "stories" } });

            //act
            IHttpActionResult actionResult = await controller.Get(1, 5);
            var contentResult = actionResult as OkNegotiatedContentResult<StoriesResource>;
            Assert.NotNull(contentResult.Content);
            Assert.Equal<long>(5,contentResult.Content.Stories.Count);

        }

        [Fact]
        public async void ShouldReturnStoryResources()
        {
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);
            var controller = new StoriesController(store);
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/stories")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary {{ "controller", "stories" } });

            //act
            IHttpActionResult actionResult = await controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<StoryResource>;
            Assert.NotNull(contentResult);
            Assert.Equal<long>(1, contentResult.Content.StoryId);
        }

        [Fact]
        public async void ShouldPostStorySuccessfully()
        {
            var db = DbTestHelper.MockDbStore();
            var store = new StoryStore<Story>(db);
            var controller = new StoriesController(store);
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/stories")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary {{ "controller", "stories" } });

            // act
            StoryModel story = new StoryModel() {
                StoryId = 650,
                Title = "Muha",
                Description = "muha",
                PublishedAt = DateTime.UtcNow,
            };

            IHttpActionResult actionResult = await controller.Post(story);
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<StoryResource>;
            Assert.NotNull(contentResult);
            Assert.Equal<string>("Muha", contentResult.Content.Title);
            Assert.Equal("defaultApi", contentResult.RouteName,ignoreCase:true); 
        }

    }
}
