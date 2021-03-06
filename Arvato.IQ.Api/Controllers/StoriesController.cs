﻿using Arvato.IQ.Api.Models;
using Arvato.IQ.Core.Stores;
using Arvato.IQ.Data;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Arvato.IQ.Api.Controllers
{
    /// <summary>
    /// expose stories api functions
    /// </summary>
    public class StoriesController : ApiController
    {
        private ResourcesFactory resourceFactory;

        protected ResourcesFactory ResourceFactory {
            get {
                if (resourceFactory == null)
                {
                    resourceFactory = new ResourcesFactory(Request);
                }
                return resourceFactory;
            }
        }


        /// <summary>
        /// constructor takes IStoryStore
        /// </summary>
        /// <param name="store"></param>
        public StoriesController(IStoryStore<Story, long> store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            Store = store;
        }

        /// <summary>
        /// story store
        /// </summary>
        protected IStoryStore<Story, long> Store { get; private set; }

        /// <summary>
        /// Get all stories paged
        /// </summary>
        /// <param name="page"> page index</param>
        /// <param name="take"> stories per page </param>
        /// <returns>stories</returns> 
        /// <response code="200">Resource found successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(StoriesResource))]
        public async Task<IHttpActionResult> Get([FromUri] int page, [FromUri] int take)
        {
            if (page <= 0)
            {
                //sure we can create our own error object for api clients but it is demo :)
                ModelState.AddModelError("paging", "page should >= 1");
                return BadRequest(ModelState);
            }
            try
            {
                var data = await Store.FindAsync(page, take, "storyId asc");
                var total = await Store.CountAsync();
                var pages = (double)total / take;
                if (page > pages)
                {
                    return NotFound();
                }
                var stories = ResourceFactory.CreatePaged(data, page, take, total);
                return Ok(stories);
            }
            catch (Exception ex)
            {
                 return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get story by id
        /// </summary>
        /// <param name="id">story id</param>
        /// <returns></returns>
        /// <response code="200">Resource found successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(StoryResource))]
        public async Task<IHttpActionResult> Get([FromUri] int id)
        {
            try
            {
                var data = await Store.FindByIdAsync(id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(this.ResourceFactory.Create(data));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        /// <summary>
        /// Create new story
        /// </summary>
        /// <param name="story"></param>
        /// <returns>created new resource</returns>
        /// <response code="201">Created successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(StoryResource))]
        public async Task<IHttpActionResult> Post([FromBody] StoryModel story)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var storyEntity = new Story() { Title = story.Title, Description = story.Description, PublishedAt = story.PublishedAt.ToUniversalTime() };
                    await Store.CreateAsync(storyEntity);
                    return CreatedAtRoute("defaultApi", new { id = storyEntity.StoryId }, ResourceFactory.Create(storyEntity));
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            return BadRequest(ModelState);
        }


        /// <summary>
        /// Update story
        /// </summary>
        /// <param name="story"></param>
        /// <returns>Updated or created new resource</returns>
        /// <response code="204">Updated successfully</response>
        /// <response code="201">Created successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(StoryResource))]
        public async Task<IHttpActionResult> Put([FromBody] StoryModel story)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var storyEntity = await Store.FindByIdAsync(story.StoryId);
                    if (storyEntity != null)
                    {
                        storyEntity.Title = story.Title;
                        storyEntity.Description = story.Description;
                        story.PublishedAt = story.PublishedAt;
                        await Store.UpdateAsync(storyEntity);
                        return StatusCode(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        storyEntity = new Story() { Title = story.Title, Description = story.Description, PublishedAt = story.PublishedAt.ToUniversalTime() };
                        await Store.CreateAsync(storyEntity);
                        return CreatedAtRoute("defaultApi", new { id = story.StoryId }, ResourceFactory.Create(storyEntity));
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            return BadRequest(ModelState);
        }


        /// <summary>
        /// Delete story using its Id
        /// </summary>
        /// <param name="id">story id</param>
        /// <returns></returns>
        /// <response code="204">Deleted successfully</response>
        /// <response code="404">Resource not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromUri] long id)
        {
            try
            {
                var story = await Store.FindByIdAsync(id);
                if (story == null)
                {
                    return NotFound();
                }
                await Store.DeleteAsync(story);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search stories 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        [HttpGet] 
        [Route("api/Stories/search")]
        public async Task<IHttpActionResult> Search([FromUri]string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return BadRequest("search term shouldn't be null or empty");
            }
            try
            {
                var result = await Store.Search(term);
                return Ok(ResourceFactory.Create(result));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
