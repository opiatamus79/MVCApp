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

            var x = uniqueList.ToList();


            if (uniqueList != null)
            {
                return uniqueList.ToList();
            }
            return emptyList;
        }

        public void InsertEmployeeContractChanges(EmployeeContractChanges contract)
        {
            EmpContractChangesDbContext.EmployeeContractChanges.Add(contract);
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
            EmpContractChangesDbContext.SaveChanges();

            
        }
    }
}