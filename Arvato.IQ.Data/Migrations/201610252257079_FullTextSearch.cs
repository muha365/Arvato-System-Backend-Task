namespace Arvato.IQ.Data.Migrations
{
    using Helpers;
    using System;
    using System.Data.Entity.Migrations;

    public partial class FullTextSearch : DbMigration
    {
        public override void Up()
        {
            this.CreateFullTextIndex("dbo.Stories",
                "PK_dbo.Stories",
                new[] { "Title", "Description" });
        }
        
        public override void Down()
        {
        }
    }
}
