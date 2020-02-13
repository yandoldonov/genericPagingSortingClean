namespace dbPersistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.dbItemTypeOnes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        guid = c.String(),
                        name = c.String(),
                        description = c.String(),
                        decimalData = c.Decimal(nullable: false, precision: 18, scale: 2),
                        boolvalue = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.dbItemTypeTwoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        guid = c.String(),
                        name = c.String(),
                        description = c.String(),
                        stringValueOne = c.String(),
                        stringValueTwo = c.String(),
                        decValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        intVlue = c.Int(nullable: false),
                        invFieldTwo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.dbItemTypeTwoes");
            DropTable("dbo.dbItemTypeOnes");
        }
    }
}
