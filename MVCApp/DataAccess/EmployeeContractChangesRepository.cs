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
        public void InsertEmployeeContractChanges(EmployeeContractChanges contract, int employeeID)
        {


            Employee employeeToUpdate = EmpContractChangesDbContext.Employees
                                        .FirstOrDefault(x => x.ID == employeeID);




            //Only supposed to do this when user is filling out survey.
            employeeToUpdate.LastUpdate = DateTime.Today;

            EmployeeContractChanges eCC = new EmployeeContractChanges();

            ///Determine if Users if filling out survey or HR worker is editing Employee Contract.
            ///

            //Check if contract with given id exists, if it does do block 2, if it does not do block 1 and 2.
            bool contractExists = EmpContractChangesDbContext
                                 .EmployeeContractChanges.Find(contract.ID) != null ? true : false;

            if(!contractExists)
            {//1 - Brand new entry into EmployeeContractChange table.
                LegalForm legalform = new LegalForm();
                eCC.LegalForm = legalform;
                eCC.LegalFormsID = legalform.ID;
                eCC.StatusID = EmpContractChangesDbContext.FormStatuses.Where(x => x.ID == contract.StatusID).FirstOrDefault().ID;
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

                EmpContractChangesDbContext.SaveChanges();
            }
            EmployeeContractChanges eCC_NEW = new EmployeeContractChanges();
            
            ///2 
            eCC_NEW.LegalForm = new LegalForm();
            eCC_NEW.StatusID = EmpContractChangesDbContext.FormStatuses.Where(x => x.ID == contract.StatusID).FirstOrDefault().ID;
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



            EmpContractChangesDbContext.SaveChanges();

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
    }
}