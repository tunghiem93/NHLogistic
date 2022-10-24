namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InnitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Order_no_id = c.String(),
                        Items = c.String(),
                        Quantity = c.Int(nullable: false),
                        Order_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Order_no = c.String(),
                        Customer_name = c.String(),
                        Receive_address = c.String(),
                        Total_amount = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
