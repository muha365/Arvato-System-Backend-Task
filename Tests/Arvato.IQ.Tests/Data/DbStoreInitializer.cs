using Arvato.IQ.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Arvato.IQ.Tests.Data
{
    internal class DbStoreInitializer : DropCreateDatabaseAlways<DbStore>
    {
        protected override void Seed(DbStore context)
        {
            string[] uris = new string[]
            {
                "http://feeds.bbci.co.uk/news/rss.xml?edition=asia",
                "http://feeds.bbci.co.uk/news/world/rss.xml",
                "http://feeds.bbci.co.uk/news/world/australia/rss.xml",
                "http://feeds.bbci.co.uk/sport/football/rss.xml"
            };


            List<Story> bulk = new List<Story>();
            for (int i = 0; i < uris.Length; i++)
            {
                using (XmlReader xmlReader = XmlReader.Create(uris[i]))
                {
                    var rss = SyndicationFeed.Load(xmlReader);
                    int max = 25;
                    int start = 0;
                    foreach (var item in rss.Items)
                    {
                        if (start < max)
                        {
                            bulk.Add(new Story()
                            {
                                Title = item.Title.Text,
                                Description = item.Summary.Text,
                                PublishedAt = item.PublishDate.UtcDateTime
                            });
                        }
                        start++;
                    }
                }
            }

            if (bulk.Count > 0)
            {
                context.Set<Story>().AddRange(bulk);
                context.SaveChanges();
            }
        }
    }

}
