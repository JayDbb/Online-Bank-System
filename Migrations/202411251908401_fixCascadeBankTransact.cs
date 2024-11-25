namespace Online_Bank_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCascadeBankTransact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankTransactions",
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
                .ForeignKey("dbo.Users", t => t.SenderAccountID)
                .Index(t => t.SenderAccountID)
                .Index(t => t.ReceiverAccountID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankTransactions", "SenderAccountID", "dbo.Users");
            DropForeignKey("dbo.BankTransactions", "ReceiverAccountID", "dbo.Accounts");
            DropIndex("dbo.BankTransactions", new[] { "ReceiverAccountID" });
            DropIndex("dbo.BankTransactions", new[] { "SenderAccountID" });
            DropTable("dbo.BankTransactions");
        }
    }
}
