namespace MVCApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContractChangeLogs2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractChanges",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UpdatedOn = c.DateTime(nullable: false),
                        EmployeeContractChange_ID = c.Int(),
                        LegalForms_ID = c.Int(),
                        Status_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmployeeContractChanges", t => t.EmployeeContractChange_ID)
                .ForeignKey("dbo.LegalForms", t => t.LegalForms_ID)
                .ForeignKey("dbo.FormStatus", t => t.Status_ID)
                .Index(t => t.EmployeeContractChange_ID)
                .Index(t => t.LegalForms_ID)
                .Index(t => t.Status_ID);
            
            DropTable("dbo.EmployeeCurrentContractInfoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmployeeCurrentContractInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NewEmail = c.String(),
                        NewLastName = c.String(),
                        NewAddress = c.String(),
                        NewCity = c.String(),
                        NewState = c.String(),
                        NewZipcode = c.Int(nullable: false),
                        NewCountry = c.String(),
                        NewHomephone = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.Int(nullable: false),
                        Country = c.String(),
                        Homephone = c.String(),
                        LastUpdateOnSurvery = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.ContractChanges", "Status_ID", "dbo.FormStatus");
            DropForeignKey("dbo.ContractChanges", "LegalForms_ID", "dbo.LegalForms");
            DropForeignKey("dbo.ContractChanges", "EmployeeContractChange_ID", "dbo.EmployeeContractChanges");
            DropIndex("dbo.ContractChanges", new[] { "Status_ID" });
            DropIndex("dbo.ContractChanges", new[] { "LegalForms_ID" });
            DropIndex("dbo.ContractChanges", new[] { "EmployeeContractChange_ID" });
            DropTable("dbo.ContractChanges");
        }
    }
}
