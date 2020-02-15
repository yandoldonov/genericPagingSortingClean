namespace dbPersistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemThree : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.dbItemTypeThrees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        guid = c.String(),
                        name = c.String(),
                        description = c.String(),
                        dataTypeEnum = c.Int(nullable: false),
                        indicatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.dbItemTypeThrees");
        }
    }
}
