namespace InventoryManagement.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Inventory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        NumberOfCartons = c.Int(nullable: false),
                        NumberOfPieces = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CartonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("dbo.Cartons", t => t.CartonId, cascadeDelete: true)
                .Index(t => t.CartonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "CartonId", "dbo.Cartons");
            DropIndex("dbo.Inventories", new[] { "CartonId" });
            DropTable("dbo.Inventories");
        }
    }
}
