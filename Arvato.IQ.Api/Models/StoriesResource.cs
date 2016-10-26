using Arvato.IQ.Core.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arvato.IQ.Api.Models
{
    /// <summary>
    /// exposes stories linked resource array
    /// </summary>
    public class StoriesResource : ApiResource
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public StoriesResource() : base()
        {
            Stories = new List<StoryResource>();
        }

        /// <summary>
        /// LinkedResource array of stories 
        /// </summary>
        public List<StoryResource> Stories { get; set; }
    }
}