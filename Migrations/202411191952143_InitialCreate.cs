namespace Online_Bank_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        SenderAccountID = c.Int(nullable: false),
                        ReceiverAccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.ReceiverAccountID)
                .ForeignKey("dbo.Accounts", t => t.SenderAccountID)
                .Index(t => t.SenderAccountID)
                .Index(t => t.ReceiverAccountID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Transactions", "SenderAccountID", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "ReceiverAccountID", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "ReceiverAccountID" });
            DropIndex("dbo.Transactions", new[] { "SenderAccountID" });
            DropIndex("dbo.Accounts", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Transactions");
            DropTable("dbo.Accounts");
        }
    }
}
