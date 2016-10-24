namespace Arvato.IQ.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TitleIX : DbMigration
    {
        private readonly string IndexName = "TitleIX";

        public override void Up()
        {
            Sql(String.Format(@"CREATE NONCLUSTERED INDEX [{0}]
                               ON [dbo].[Stories] ([Title])
                               INCLUDE (PublishedAt)", IndexName));
        }

        public override void Down()
        {

            DropIndex("dbo.Stories", IndexName);
        }

    }
}
