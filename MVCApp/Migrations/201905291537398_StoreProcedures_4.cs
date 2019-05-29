namespace MVCApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreProcedures_4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContractChanges", "EmployeeContractChange_ID", "dbo.EmployeeContractChanges");
            DropForeignKey("dbo.ContractChanges", "LegalForms_ID", "dbo.LegalForms");
            DropForeignKey("dbo.ContractChanges", "Status_ID", "dbo.FormStatus");
            DropIndex("dbo.ContractChanges", new[] { "EmployeeContractChange_ID" });
            DropIndex("dbo.ContractChanges", new[] { "LegalForms_ID" });
            DropIndex("dbo.ContractChanges", new[] { "Status_ID" });
            AddColumn("dbo.ContractChanges", "NewLastName", c => c.String());
            AddColumn("dbo.ContractChanges", "NewEmail", c => c.String());
            AddColumn("dbo.ContractChanges", "NewAddress", c => c.String());
            AddColumn("dbo.ContractChanges", "NewCity", c => c.String());
            AddColumn("dbo.ContractChanges", "NewState", c => c.String());
            AddColumn("dbo.ContractChanges", "NewZipcode", c => c.Int(nullable: false));
            AddColumn("dbo.ContractChanges", "NewCountry", c => c.String());
            AddColumn("dbo.ContractChanges", "NewHomePhone", c => c.String());
            AddColumn("dbo.ContractChanges", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.ContractChanges", "StatusID", c => c.Int(nullable: false));
            AddColumn("dbo.ContractChanges", "LegalFormsID", c => c.Int(nullable: false));
            AddColumn("dbo.ContractChanges", "EmployeeID", c => c.Int(nullable: false));
            AddColumn("dbo.ContractChanges", "ChangeLogID", c => c.Int(nullable: false));
            AddColumn("dbo.ContractChanges", "StatusName", c => c.String());
            AddColumn("dbo.ContractChanges", "Description", c => c.String());
            AddColumn("dbo.ContractChanges", "FilePath", c => c.String());
            AddColumn("dbo.ContractChanges", "Reason", c => c.String());
            DropColumn("dbo.ContractChanges", "EmployeeContractChange_ID");
            DropColumn("dbo.ContractChanges", "LegalForms_ID");
            DropColumn("dbo.ContractChanges", "Status_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContractChanges", "Status_ID", c => c.Int());
            AddColumn("dbo.ContractChanges", "LegalForms_ID", c => c.Int());
            AddColumn("dbo.ContractChanges", "EmployeeContractChange_ID", c => c.Int());
            DropColumn("dbo.ContractChanges", "Reason");
            DropColumn("dbo.ContractChanges", "FilePath");
            DropColumn("dbo.ContractChanges", "Description");
            DropColumn("dbo.ContractChanges", "StatusName");
            DropColumn("dbo.ContractChanges", "ChangeLogID");
            DropColumn("dbo.ContractChanges", "EmployeeID");
            DropColumn("dbo.ContractChanges", "LegalFormsID");
            DropColumn("dbo.ContractChanges", "StatusID");
            DropColumn("dbo.ContractChanges", "DateCreated");
            DropColumn("dbo.ContractChanges", "NewHomePhone");
            DropColumn("dbo.ContractChanges", "NewCountry");
            DropColumn("dbo.ContractChanges", "NewZipcode");
            DropColumn("dbo.ContractChanges", "NewState");
            DropColumn("dbo.ContractChanges", "NewCity");
            DropColumn("dbo.ContractChanges", "NewAddress");
            DropColumn("dbo.ContractChanges", "NewEmail");
            DropColumn("dbo.ContractChanges", "NewLastName");
            CreateIndex("dbo.ContractChanges", "Status_ID");
            CreateIndex("dbo.ContractChanges", "LegalForms_ID");
            CreateIndex("dbo.ContractChanges", "EmployeeContractChange_ID");
            AddForeignKey("dbo.ContractChanges", "Status_ID", "dbo.FormStatus", "ID");
            AddForeignKey("dbo.ContractChanges", "LegalForms_ID", "dbo.LegalForms", "ID");
            AddForeignKey("dbo.ContractChanges", "EmployeeContractChange_ID", "dbo.EmployeeContractChanges", "ID");
        }
    }
}
