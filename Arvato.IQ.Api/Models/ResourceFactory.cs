using Arvato.IQ.Core.Api;
using Arvato.IQ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Arvato.IQ.Api.Models
{
    /// <summary>
    /// Model Factory for handling api models customization
    /// </summary>
    public class ResourcesFactory
    {
        private System.Web.Http.Routing.UrlHelper _UrlHelper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="request"></param>
        public ResourcesFactory(HttpRequestMessage request)
        {
            _UrlHelper = new System.Web.Http.Routing.UrlHelper(request);
        }


        /// <summary>
        /// Create resource for story
        /// </summary>
        /// <param name="story">story entity</param>
        /// <returns>a story linked resource model</returns>
        public StoryResource Create(Story story)
        {
            return new StoryResource()
            {
                StoryId = story.StoryId,
                Title = story.Title,
                Description = story.Description,
                PublishedAt = story.PublishedAt,
                Links = new List<ResourceLink>() {
                    new ResourceLink() {
                        Rel = "self",
                        Method ="GET",
                        Href = _UrlHelper.Route("defaultApi", new { controller = "Stories", id = story.StoryId })
                    },
                    new ResourceLink() {
                        Rel = "Delete",
                        Method ="DELETE",
                        Href = _UrlHelper.Route("defaultApi", new { controller = "Stories", id = story.StoryId })
                    },
                    new ResourceLink() {
                        Rel = "Update",
                        Method ="PUT",
                        Href = _UrlHelper.Route("defaultApi", new { controller = "Stories", id = story.StoryId })
                    }
                }
            };
        }

        /// <summary>
        /// Create resource for stories 
        /// </summary>
        /// <param name="stories">set of story entities</param>
        /// <returns> Linked resources of story modes</returns>
        public StoriesResource Create(IEnumerable<Story> stories)
        {
            var data = new StoriesResource();
            foreach (var item in stories)
            {
                data.Stories.Add(Create(item));
            }
            return data;
        }

        /// <summary>
        /// Create Resource for paged stories
        /// </summary>
        /// <param name="stories"></param>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public StoriesResource CreatePaged(IEnumerable<Story> stories, int page, int take, long total)
        {
            var data = Create(stories);
            var totalPages = Math.Ceiling(((double)total / take));
            if (page < totalPages)
            {
                data.Links.Add(new ResourceLink()
                {
                    Rel = "next",
                    Href = _UrlHelper.Route("DefaultApi", new {controller="stories", page = page + 1, take = take })
                });

            }
            if (page != totalPages && page > 1)
            {
                data.Links.Add(new ResourceLink()
                {
                    Rel = "prev",
                    Href = _UrlHelper.Route("DefaultApi", new {controller="stories", page = page - 1, take = take })
                });
            }
            data.Links.Add(new ResourceLink()
            {
                Rel = "self",
                Href = _UrlHelper.Route("DefaultApi", new { controller = "stories", page = page, take = take })
            });
            return data;
        }


        public HomeResource CreateHomeResource(string title, string description)
        {
            var homeResource = new HomeResource(){
                Title = title,
                Description =description 
            };
            homeResource.Links.Add(new ResourceLink()
            {
                Rel ="stories",
                Href = _UrlHelper.Route("DefaultApi", new {controller="stories", page = 1, take = 20 })
                ,Method = "GET"
            });
            return homeResource;
        }
    }
}