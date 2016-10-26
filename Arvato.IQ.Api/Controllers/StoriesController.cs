using Arvato.IQ.Api.Models;
using Arvato.IQ.Core.Entities;
using Arvato.IQ.Core.Stores;
using Arvato.IQ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Arvato.IQ.Api.Controllers
{
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
        public StoriesController(IStoryStore<Story,long> store)
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
        protected IStoryStore<Story,long> Store { get; private set; }

        /// <summary>
        /// Get all stories paged
        /// </summary>
        /// <param name="page"> page index</param>
        /// <param name="take"> stories per page </param>
        /// <returns>stories</returns> 
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(StoriesResource))]
        public async Task<IHttpActionResult> Get(int page, int take)
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
                // we will log error here & trace it 
                //Configuration.Services.GetTraceWriter()
                //.Info(Request, 
                //this.ControllerContext.ControllerDescriptor.ControllerType.FullName,
                //"Get the list of Stories exception {0}.",ex.Message);
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
        [ResponseType(typeof(Models.StoryResource))]
        public async Task<IHttpActionResult> Post(Story story)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await Store.CreateAsync(story);
                    return CreatedAtRoute("default", new { id = story.StoryId }, ResourceFactory.Create(story));
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(long id)
        {
            try
            {
                var story = await Store.FindByIdAsync(id);
                if (story == null)
                {
                    return NotFound();
                }
                await Store.DeleteAsync(story);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
