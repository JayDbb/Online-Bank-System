namespace Online_Bank_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Password", c => c.String());
            AddColumn("dbo.Accounts", "IsHidden", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "IsHidden");
            DropColumn("dbo.Accounts", "Password");
        }
    }
}
