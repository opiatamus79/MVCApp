namespace MVCApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoredProcedures : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
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
                    },
                body:
                    @"INSERT [dbo].[EmployeeContractChanges]([NewLastName], [NewEmail], [NewAddress], [NewCity], [NewState], [NewZipcode], [NewCountry], [NewHomePhone], [DateCreated], [ChangeLogID], [StatusID], [LegalFormsID], [EmployeeID])
                      VALUES (@NewLastName, @NewEmail, @NewAddress, @NewCity, @NewState, @NewZipcode, @NewCountry, @NewHomePhone, @DateCreated, @ChangeLogID, @StatusID, @LegalFormsID, @EmployeeID)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[EmployeeContractChanges]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[EmployeeContractChanges] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
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
                    },
                body:
                    @"UPDATE [dbo].[EmployeeContractChanges]
                      SET [NewLastName] = @NewLastName, [NewEmail] = @NewEmail, [NewAddress] = @NewAddress, [NewCity] = @NewCity, [NewState] = @NewState, [NewZipcode] = @NewZipcode, [NewCountry] = @NewCountry, [NewHomePhone] = @NewHomePhone, [DateCreated] = @DateCreated, [ChangeLogID] = @ChangeLogID, [StatusID] = @StatusID, [LegalFormsID] = @LegalFormsID, [EmployeeID] = @EmployeeID
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.EmployeeContractChanges_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[EmployeeContractChanges]
                      WHERE ([ID] = @ID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.EmployeeContractChanges_Delete");
            DropStoredProcedure("dbo.EmployeeContractChanges_Update");
            DropStoredProcedure("dbo.EmployeeContractChanges_Insert");
        }
    }
}
