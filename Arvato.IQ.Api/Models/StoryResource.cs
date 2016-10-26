using Arvato.IQ.Core.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arvato.IQ.Api.Models
{
    /// <summary>
    /// Represent story resource resource type
    /// </summary>
    public class StoryResource : ApiResource
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public StoryResource() : base()
        {  
        }

        /// <summary>
        /// the story identifier
        /// </summary>
        public long StoryId { get; set; }

        /// <summary>
        /// the story title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// the story description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// the publish date & time
        /// </summary>
        public DateTime PublishedAt { get; set; }
    }
}