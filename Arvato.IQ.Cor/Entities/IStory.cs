using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Cor.Entities
{
    /// <summary>
    /// Represent default Story contract with default Tkey type as System.Int64
    /// </summary>
    public interface IStory : IStory<long>
    {

    }

    /// <summary>
    /// Represent Story core contract with generic primary key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IStory<out TKey>
        where TKey : IEquatable<TKey>
    {

        /// <summary>
        /// Story key
        /// </summary>
        TKey StoryId { get; }

        /// <summary>
        /// story title
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// story description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// story publish date and time
        /// </summary>
        DateTime PublishedAt { get; set; }

    }
}
