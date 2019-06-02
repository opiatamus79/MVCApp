namespace MVCApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFormType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeContractChanges", "FormType", c => c.String());
            AlterStoredProcedure(
                "dbo.EmployeeContractChanges_Insert",
                p => new
                    {
                        NewLastName = p.String(maxLength: 50),
                        NewEmail = p.String(maxLength: 50),
                        NewAddress = p.String(maxLength: 50),
                        NewCity = p.String(maxLength: 50),
                        NewState = p.String(maxLength: 50),
                        NewZipcode = p.Int(),
                        NewCountry = p.String(maxLength: 50),
                        NewHomePhone = p.String(maxLength: 20),
                        DateCreated = p.DateTime(storeType: "date"),
                        ChangeLogID = p.Int(),
                        StatusID = p.Int(),
                        LegalFormsID = p.Int(),
                        EmployeeID = p.Int(),
                        FormType = p.String(),
                    },
                body:
                    @"INSERT [dbo].[EmployeeContractChanges]([NewLastName], [NewEmail], [NewAddress], [NewCity], [NewState], [NewZipcode], [NewCountry], [NewHomePhone], [DateCreated], [ChangeLogID], [StatusID], [LegalFormsID], [EmployeeID], [FormType])
                      VALUES (@NewLastName, @NewEmail, @NewAddress, @NewCity, @NewState, @NewZipcode, @NewCountry, @NewHomePhone, @DateCreated, @ChangeLogID, @StatusID, @LegalFormsID, @EmployeeID, @FormType)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[EmployeeContractChanges]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[EmployeeContractChanges] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            AlterStoredProcedure(
                "dbo.EmployeeContractChanges_Update",
                p => new
                    {
                        ID = p.Int(),
                        NewLastName = p.String(maxLength: 50),
                        NewEmail = p.String(maxLength: 50),
                        NewAddress = p.String(maxLength: 50),
                        NewCity = p.String(maxLength: 50),
                        NewState = p.String(maxLength: 50),
                        NewZipcode = p.Int(),
                        NewCountry = p.String(maxLength: 50),
                        NewHomePhone = p.String(maxLength: 20),
                        DateCreated = p.DateTime(storeType: "date"),
                        ChangeLogID = p.Int(),
                        StatusID = p.Int(),
                        LegalFormsID = p.Int(),
                        EmployeeID = p.Int(),
                        FormType = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[EmployeeContractChanges]
                      SET [NewLastName] = @NewLastName, [NewEmail] = @NewEmail, [NewAddress] = @NewAddress, [NewCity] = @NewCity, [NewState] = @NewState, [NewZipcode] = @NewZipcode, [NewCountry] = @NewCountry, [NewHomePhone] = @NewHomePhone, [DateCreated] = @DateCreated, [ChangeLogID] = @ChangeLogID, [StatusID] = @StatusID, [LegalFormsID] = @LegalFormsID, [EmployeeID] = @EmployeeID, [FormType] = @FormType
                      WHERE ([ID] = @ID)"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeContractChanges", "FormType");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
