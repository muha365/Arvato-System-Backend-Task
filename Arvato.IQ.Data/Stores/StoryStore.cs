using Arvato.IQ.Core.Entities;
using Arvato.IQ.Core.Stores;
using Arvato.IQ.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Data.Stores
{
    /// <summary>
    /// Default implementation of story store 
    /// </summary>
    /// <typeparam name="TStory"></typeparam>
    public class StoryStore<TStory> : StoryStore<TStory, long>, IStoryStore<TStory, long>
        where TStory : class, IStory<long>, new()
    {
        public StoryStore(DbStore store) : base(store)
        {
        }
    }


    /// <summary>
    /// Core implementation for the story store
    /// </summary>
    /// <typeparam name="TStory"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class StoryStore<TStory, TKey> : Store<TStory, TKey>, IStoryStore<TStory, TKey>
        where TStory : class, IStory<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        public StoryStore(DbStore store) : base(store)
        {
        }

        public async Task<IEnumerable<TStory>> Search(string term)
        {
            DbStore db = DbStore as DbStore;
            if (db == null)
            {
                throw new Exception("DbStore is null");
            }
            var table = db.GetTableName(typeof(TStory));
            var sql = string.Format("SELECT * FROM {0} WHERE CONTAINS((Title, Description), @p0)", table);
            return await db.Set<TStory>().SqlQuery(sql, term).ToListAsync();
        }
    }
}
