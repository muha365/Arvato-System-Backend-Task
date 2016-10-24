namespace Arvato.IQ.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        StoryId = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Description = c.String(),
                        PublishedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stories");
        }
    }
}
