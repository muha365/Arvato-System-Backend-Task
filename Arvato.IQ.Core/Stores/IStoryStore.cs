using Arvato.IQ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Core.Stores
{
    /// <summary>
    /// interface that exposes story store basic api with key type of int64.  
    /// </summary>
    /// <typeparam name="TStory"></typeparam>
    public interface IStoryStore<TStory> : IStoryStore<TStory, long>, IStore<TStory, long>
        where TStory : class, IStory<long>, new()
    {

    }

    /// <summary>
    /// interface that exposes story store basic api with generic key type.  
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IStoryStore<TEntity, in TKey> : IStore<TEntity, TKey>
       where TEntity : class, IStory<TKey>, new()
        where TKey : IEquatable<TKey>
    {

    }
}