namespace Restorizer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.DishHasIngredients",
                c => new
                    {
                        DishId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        AmountInG = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DishId, t.IngredientId })
                .ForeignKey("dbo.Dishes", t => t.DishId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .Index(t => t.DishId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SupplierHasIngredients",
                c => new
                    {
                        SupplierId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        PricePerKg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SupplierId, t.IngredientId })
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Supplies",
                c => new
                    {
                        SupplierId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        PricePerKg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SupplierId, t.IngredientId, t.Date })
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.OrderHasDishes",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        DishId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.DishId })
                .ForeignKey("dbo.Dishes", t => t.DishId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.DishId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderHasDishes", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderHasDishes", "DishId", "dbo.Dishes");
            DropForeignKey("dbo.Supplies", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Supplies", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.SupplierHasIngredients", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.SupplierHasIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.DishHasIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.DishHasIngredients", "DishId", "dbo.Dishes");
            DropForeignKey("dbo.Dishes", "Category_Id", "dbo.Categories");
            DropIndex("dbo.OrderHasDishes", new[] { "DishId" });
            DropIndex("dbo.OrderHasDishes", new[] { "OrderId" });
            DropIndex("dbo.Supplies", new[] { "IngredientId" });
            DropIndex("dbo.Supplies", new[] { "SupplierId" });
            DropIndex("dbo.SupplierHasIngredients", new[] { "IngredientId" });
            DropIndex("dbo.SupplierHasIngredients", new[] { "SupplierId" });
            DropIndex("dbo.DishHasIngredients", new[] { "IngredientId" });
            DropIndex("dbo.DishHasIngredients", new[] { "DishId" });
            DropIndex("dbo.Dishes", new[] { "Category_Id" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderHasDishes");
            DropTable("dbo.Supplies");
            DropTable("dbo.Suppliers");
            DropTable("dbo.SupplierHasIngredients");
            DropTable("dbo.Ingredients");
            DropTable("dbo.DishHasIngredients");
            DropTable("dbo.Dishes");
            DropTable("dbo.Categories");
        }
    }
}
