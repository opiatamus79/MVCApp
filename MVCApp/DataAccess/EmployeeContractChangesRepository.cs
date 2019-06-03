using MVCApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.DataAccess
{
    public class EmployeeContractChangesRepository
    {
        AuthenticateContext EmpContractChangesDbContext = new AuthenticateContext();



        public EmployeeContractChanges GetEmployeeContractChangesByID(int id)
        {
            return EmpContractChangesDbContext.EmployeeContractChanges.Find(id);
        }

        public List<ContractChanges> GetUniqueEmployeeContractLogs()//will be using stored procedure.
        {
            List<ContractChanges> emptyList = new List<ContractChanges>();
            var uniqueList = EmpContractChangesDbContext.Database.SqlQuery<ContractChanges>("RetrieveUniqueChangeLogs");

            if (uniqueList != null)
            {
                return uniqueList.ToList();
            }
            return emptyList;
        }

        /*public void InsertEmployeeContractChanges(EmployeeContractChanges contract)
        {
            EmpContractChangesDbContext.EmployeeContractChanges.Add(contract);
            EmpContractChangesDbContext.SaveChanges();
        }*/
        public IEnumerable<ContractChanges> getChangeHistory(int changeLogID, int employeeID)
        {//Will retrieve the 
            AuthenticateContext db = EmpContractChangesDbContext;

            var result = (from c in db.EmployeeContractChanges.AsEnumerable()
             join status in db.FormStatuses.AsEnumerable() on c.StatusID equals status.ID
             join legal in db.LegalForms.AsEnumerable() on c.LegalFormsID equals legal.ID
             where (c.ChangeLogID == changeLogID && c.EmployeeID == employeeID)
             select new ViewModels.ContractChanges
             {
                 ID = c.ID,
                 NewLastName = c.NewLastName,
                 NewEmail = c.NewEmail,
                 NewAddress = c.NewAddress,
                 NewCity = c.NewCity,
                 NewState = c.NewState,
                 NewZipcode = c.NewZipcode,
                 NewCountry = c.NewCountry,
                 NewHomePhone = c.NewHomePhone,
                 DateCreated = c.DateCreated,
                 StatusID = c.StatusID,
                 LegalFormsID = c.LegalFormsID,
                 EmployeeID = c.EmployeeID,
                 ChangeLogID = c.ChangeLogID,
                 StatusName = c.FormStatus.StatusName,
                 Description = c.FormStatus.Description,
                 FilePath = c.LegalForm.FilePath,
                 Reason = c.LegalForm.Reason,
                 UpdatedOn = c.DateCreated

             });
            return result;
           
        }
        public void InsertEmployeeContractChanges(EmployeeContractChanges contract, int employeeID)
        {
            Employee employeeToUpdate = EmpContractChangesDbContext.Employees
                                        .FirstOrDefault(x => x.ID == employeeID);

            //Only supposed to do this when user is filling out survey.
            employeeToUpdate.LastUpdate = DateTime.Now;

            EmployeeContractChanges eCC = new EmployeeContractChanges();
            EmployeeContractChanges lastCF = GetLCF(employeeToUpdate.ID);
            
            //Check if contract with given id exists, if it does do block 2, if it does not do block 1 and 2.

            bool contractExists = EmpContractChangesDbContext
                                 .EmployeeContractChanges.Find(lastCF.ID) != null ? true : false;

            if(!contractExists)
            {//1 - Brand new entry into EmployeeContractChange table.
                LegalForm legalform = new LegalForm();
                eCC.LegalForm = legalform;
                eCC.LegalFormsID = legalform.ID;
                eCC.StatusID = 1; //WILL BE PROVIDING STATUS
                eCC.EmployeeID = employeeToUpdate.ID;
                eCC.ChangeLogID = 1 + (EmpContractChangesDbContext.EmployeeContractChanges.
                    OrderByDescending(e => e.EmployeeID == employeeToUpdate.ID).FirstOrDefault().ChangeLogID);
                eCC.NewAddress = employeeToUpdate.Address;
                eCC.NewCity = employeeToUpdate.City;
                eCC.NewCountry = employeeToUpdate.Country;
                eCC.NewEmail = employeeToUpdate.Email;
                eCC.NewHomePhone = employeeToUpdate.HomePhone;
                eCC.NewLastName = employeeToUpdate.LastName;
                eCC.NewState = employeeToUpdate.State;
                eCC.NewZipcode = employeeToUpdate.Zipcode;

                EmpContractChangesDbContext.EmployeeContractChanges.Add(eCC);
                EmpContractChangesDbContext.SaveChanges();
            }
            var b = EmpContractChangesDbContext.EmployeeContractChanges.ToList(); //DELETE ME
            EmployeeContractChanges eCC_NEW = new EmployeeContractChanges();

            ///2 
            eCC_NEW.LegalFormsID = lastCF.LegalFormsID;
            eCC_NEW.StatusID = 1; //WILL BE PROVIDING STATUS
            eCC_NEW.EmployeeID = employeeToUpdate.ID;
            eCC_NEW.ChangeLogID = (EmpContractChangesDbContext.EmployeeContractChanges.
                OrderByDescending(e => e.EmployeeID == employeeToUpdate.ID).FirstOrDefault().ChangeLogID);
            eCC_NEW.NewAddress = contract.NewAddress;
            eCC_NEW.NewCity = contract.NewCity;
            eCC_NEW.NewCountry = contract.NewCountry;
            eCC_NEW.NewEmail = contract.NewEmail;
            eCC_NEW.NewHomePhone = contract.NewHomePhone;
            eCC_NEW.NewLastName = contract.NewLastName;
            eCC_NEW.NewState = contract.NewState;
            eCC_NEW.NewZipcode = contract.NewZipcode;


            EmpContractChangesDbContext.EmployeeContractChanges.Add(eCC_NEW);
            EmpContractChangesDbContext.SaveChanges();

            b = EmpContractChangesDbContext.EmployeeContractChanges.ToList();

            Console.Write(b);

        }



        public void UpdateEmployee(Employee e)
        {
            Employee employeeToUpdate = EmpContractChangesDbContext.Employees.FirstOrDefault(x => x.ID == e.ID);

            
            employeeToUpdate.LastName = e.LastName;
            employeeToUpdate.Email = e.Email;
            employeeToUpdate.Address = e.Address;
            employeeToUpdate.City = e.City;
            employeeToUpdate.State = e.State;
            employeeToUpdate.Zipcode = e.Zipcode;
            employeeToUpdate.Country = e.Country;
            employeeToUpdate.HomePhone = e.HomePhone;
            //employeeToUpdate.LastUpdate = DateTime.Today;
            EmpContractChangesDbContext.SaveChanges();

            
        }

        public HRDashboardViewModel HRDashboardViewModel(Employee employee, string form, int statusID)
        {
            return new HRDashboardViewModel()
            {
                NewAddress = employee.Address,
                NewCity = employee.City,
                NewCountry = employee.Country,
                NewEmail = employee.Email,
                NewHomePhone = employee.HomePhone,
                NewLastName = employee.LastName,
                NewState = employee.State,
                NewZipcode = employee.Zipcode,
                StatusID = statusID,
                FormType = form
            };
        }

        public EmployeeContractChanges GetLCF(int userID)
        {//Gets Last contract change form.
            AuthenticateContext db = EmpContractChangesDbContext;
            return db.EmployeeContractChanges
                .Where(x => x.EmployeeID == userID).OrderByDescending(x => x.DateCreated).FirstOrDefault();

        }
    }
}