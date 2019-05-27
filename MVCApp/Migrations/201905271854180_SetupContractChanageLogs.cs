namespace MVCApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetupContractChanageLogs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeContractChanges", "ChangeLogID", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "DateCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "DateCreated", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.EmployeeContractChanges", "ChangeLogID");
        }
    }
}
