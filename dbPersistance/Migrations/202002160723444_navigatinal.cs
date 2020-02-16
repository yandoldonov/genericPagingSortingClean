namespace dbPersistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class navigatinal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.dbItemTypeThrees", "dbItemTypeOne_Id", c => c.Int());
            AddColumn("dbo.dbItemTypeTwoes", "dbItemTypeOne_Id", c => c.Int());
            CreateIndex("dbo.dbItemTypeThrees", "dbItemTypeOne_Id");
            CreateIndex("dbo.dbItemTypeTwoes", "dbItemTypeOne_Id");
            AddForeignKey("dbo.dbItemTypeThrees", "dbItemTypeOne_Id", "dbo.dbItemTypeOnes", "Id");
            AddForeignKey("dbo.dbItemTypeTwoes", "dbItemTypeOne_Id", "dbo.dbItemTypeOnes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.dbItemTypeTwoes", "dbItemTypeOne_Id", "dbo.dbItemTypeOnes");
            DropForeignKey("dbo.dbItemTypeThrees", "dbItemTypeOne_Id", "dbo.dbItemTypeOnes");
            DropIndex("dbo.dbItemTypeTwoes", new[] { "dbItemTypeOne_Id" });
            DropIndex("dbo.dbItemTypeThrees", new[] { "dbItemTypeOne_Id" });
            DropColumn("dbo.dbItemTypeTwoes", "dbItemTypeOne_Id");
            DropColumn("dbo.dbItemTypeThrees", "dbItemTypeOne_Id");
        }
    }
}
