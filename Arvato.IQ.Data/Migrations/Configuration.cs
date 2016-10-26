namespace Arvato.IQ.Data.Migrations
{
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.SqlServer;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Xml;

    internal sealed class Configuration : DbMigrationsConfiguration<DbStore>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations";
            SetSqlGenerator(SqlProviderServices.ProviderInvariantName, new CustomMigrationSqlGenerator());
        }

        protected override void Seed(DbStore context)
        {
            var categories = new string[] {
            "http://feeds.bbci.co.uk/news/rss.xml?edition=asia",
            "http://feeds.bbci.co.uk/news/world/rss.xml",
            "http://feeds.bbci.co.uk/news/world/australia/rss.xml",
            "http://feeds.bbci.co.uk/sport/football/rss.xml",
            "http://feeds.bbci.co.uk/news/business/rss.xml",
            "http://feeds.bbci.co.uk/news/health/rss.xml",
            "http://feeds.bbci.co.uk/news/entertainment_and_arts/rss.xml",
            "http://feeds.bbci.co.uk/news/education/rss.xml"
            };

            List<Story> bulk = new List<Story>();

            foreach (var url in categories)
            {
                using (XmlReader xmlReader = XmlReader.Create(url))
                {
                    var rss = SyndicationFeed.Load(xmlReader);
                    foreach (var item in rss.Items)
                    {
                        bulk.Add(new Story()
                        {
                            Title = item.Title.Text,
                            Description = item.Summary.Text,
                            PublishedAt = item.PublishDate.UtcDateTime
                        });
                    }
                }
                context.Set<Story>().AddRange(bulk);
            }
        }
    }
}
