namespace Restorizer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplyModelChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SupplierHasIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.SupplierHasIngredients", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Supplies", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.SupplierHasIngredients", new[] { "SupplierId" });
            DropIndex("dbo.SupplierHasIngredients", new[] { "IngredientId" });
            DropIndex("dbo.Supplies", new[] { "SupplierId" });
            DropPrimaryKey("dbo.Supplies");
            AddColumn("dbo.Dishes", "IsArchived", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredients", "PricePerKg", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Supplies", new[] { "IngredientId", "Date" });
            DropColumn("dbo.Supplies", "SupplierId");
            DropTable("dbo.SupplierHasIngredients");
            DropTable("dbo.Suppliers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Suppliers",
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
                .PrimaryKey(t => new { t.SupplierId, t.IngredientId });
            
            AddColumn("dbo.Supplies", "SupplierId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Supplies");
            DropColumn("dbo.Ingredients", "PricePerKg");
            DropColumn("dbo.Dishes", "IsArchived");
            AddPrimaryKey("dbo.Supplies", new[] { "SupplierId", "IngredientId", "Date" });
            CreateIndex("dbo.Supplies", "SupplierId");
            CreateIndex("dbo.SupplierHasIngredients", "IngredientId");
            CreateIndex("dbo.SupplierHasIngredients", "SupplierId");
            AddForeignKey("dbo.Supplies", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SupplierHasIngredients", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SupplierHasIngredients", "IngredientId", "dbo.Ingredients", "Id", cascadeDelete: true);
        }
    }
}
