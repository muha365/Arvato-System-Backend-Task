using Arvato.IQ.Cor.Entities;
using System;

namespace Arvato.IQ.Cor.Stores
{
    /// <summary>
    /// interface that exposes and represent basic story store api with default story key as int64
    /// </summary>
    /// <typeparam name="TStory"></typeparam>
    public interface IStoryStore<TStory> : IStoryStore<TStory, long>, IStore<TStory, long>
            where TStory : class, IStory<long>, new()
        {

        }

    /// <summary>
    /// interface that exposes and represent basic story store api
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IStoryStore<TEntity, in TKey> : IStore<TEntity, TKey>
           where TEntity : class, IStory<TKey>, new()
            where TKey : IEquatable<TKey>
        {

        }
    }
}
