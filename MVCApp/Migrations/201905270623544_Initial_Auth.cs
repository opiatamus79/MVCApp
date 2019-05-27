namespace MVCApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Auth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeContractChanges",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NewLastName = c.String(nullable: false, maxLength: 50),
                        NewEmail = c.String(nullable: false, maxLength: 50),
                        NewAddress = c.String(nullable: false, maxLength: 50),
                        NewCity = c.String(nullable: false, maxLength: 50),
                        NewState = c.String(nullable: false, maxLength: 50),
                        NewZipcode = c.Int(nullable: false),
                        NewCountry = c.String(nullable: false, maxLength: 50),
                        NewHomePhone = c.String(maxLength: 20),
                        DateCreated = c.DateTime(nullable: false, storeType: "date"),
                        StatusID = c.Int(nullable: false),
                        LegalFormsID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.FormStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("dbo.LegalForms", t => t.LegalFormsID, cascadeDelete: true)
                .Index(t => t.StatusID)
                .Index(t => t.LegalFormsID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StaffID = c.Int(nullable: false),
                        HRID = c.String(nullable: false, maxLength: 50),
                        LastUpdate = c.DateTime(nullable: false, storeType: "date"),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        State = c.String(nullable: false, maxLength: 50),
                        Zipcode = c.Int(nullable: false),
                        Country = c.String(nullable: false, maxLength: 50),
                        HomePhone = c.String(maxLength: 20),
                        DateCreated = c.DateTime(nullable: false, storeType: "date"),
                        Role_RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId)
                .Index(t => t.Role_RoleId);
            
            CreateTable(
                "dbo.FormStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StatusName = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LegalForms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FilePath = c.String(nullable: false, maxLength: 50),
                        Reason = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.EmployeeContractChanges", "LegalFormsID", "dbo.LegalForms");
            DropForeignKey("dbo.EmployeeContractChanges", "StatusID", "dbo.FormStatus");
            DropForeignKey("dbo.EmployeeContractChanges", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "Role_RoleId" });
            DropIndex("dbo.EmployeeContractChanges", new[] { "EmployeeID" });
            DropIndex("dbo.EmployeeContractChanges", new[] { "LegalFormsID" });
            DropIndex("dbo.EmployeeContractChanges", new[] { "StatusID" });
            DropTable("dbo.Roles");
            DropTable("dbo.EmployeeCurrentContractInfoes");
            DropTable("dbo.LegalForms");
            DropTable("dbo.FormStatus");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeContractChanges");
        }
    }
}
