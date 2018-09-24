namespace AsaderoVivaMexicoPV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Configs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dollar = c.Single(nullable: false),
                        Name = c.String(nullable: false),
                        RFC = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Phone = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        GroupID = c.String(nullable: false),
                        TableId = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Active = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Tables", t => t.TableId, cascadeDelete: true)
                .Index(t => t.TableId)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 50),
                        CategoryId = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        TableId = c.Int(nullable: false, identity: true),
                        TableNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TableId);
            
            CreateTable(
                "dbo.Spendings",
                c => new
                    {
                        SpendingID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Cost = c.Single(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SpendingID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "TableId", "dbo.Tables");
            DropForeignKey("dbo.Orders", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Orders", new[] { "ProductID" });
            DropIndex("dbo.Orders", new[] { "TableId" });
            DropTable("dbo.Spendings");
            DropTable("dbo.Tables");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.Configs");
            DropTable("dbo.Categories");
        }
    }
}
