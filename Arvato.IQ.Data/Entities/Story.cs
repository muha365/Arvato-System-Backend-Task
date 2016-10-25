using Arvato.IQ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Data
{ 

    /// <summary>
    /// Default entityframework implementation for story with the key of type Int64
    /// </summary>
    public class Story : Story<long>
    {
    }

    /// <summary>
    /// Default entityframework implementation for story 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class Story<TKey> : IStory<TKey>
        where TKey : IEquatable<TKey>
    {

        /// <summary>
        /// default constructor
        /// </summary>
        public Story()
        {
        }

        /// <summary>
        /// story Id
        /// </summary>
        public virtual TKey StoryId { get; set; }

        /// <summary>
        /// story title
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// story description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// published at
        /// </summary>
        public virtual DateTime PublishedAt { get; set; }

    }

}
