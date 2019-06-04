﻿using MVCApp.ViewModels;
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
            bool contractExists = false;
            if (lastCF != null)
            {
                contractExists = EmpContractChangesDbContext
                    .EmployeeContractChanges.Find(lastCF.ID) != null ? true : false;
            }


            LegalForm legalform = new LegalForm() { FilePath = "N/A", Reason = "N/A" };
            if (!contractExists || StatusIs(lastCF.StatusID) == "Approved" || StatusIs(lastCF.StatusID) == "Opt-out")
            {//1 - Brand new entry into EmployeeContractChange table.

                legalform = CreateNewLegalForm(legalform);

                eCC.LegalForm = legalform;
                eCC.LegalFormsID = legalform.ID;
                eCC.StatusID = 1; 
                eCC.EmployeeID = employeeToUpdate.ID;
                eCC.ChangeLogID = 1; //+ (EmpContractChangesDbContext.EmployeeContractChanges.
                    //OrderByDescending(e => e.EmployeeID == employeeToUpdate.ID).FirstOrDefault().ChangeLogID);
                eCC.NewAddress = employeeToUpdate.Address;
                eCC.NewCity = employeeToUpdate.City;
                eCC.NewCountry = employeeToUpdate.Country;
                eCC.NewEmail = employeeToUpdate.Email;
                eCC.NewHomePhone = employeeToUpdate.HomePhone;
                eCC.NewLastName = employeeToUpdate.LastName;
                eCC.NewState = employeeToUpdate.State;
                eCC.NewZipcode = employeeToUpdate.Zipcode;
                eCC.DateCreated = DateTime.Now;

                EmpContractChangesDbContext.EmployeeContractChanges.Add(eCC);
                EmpContractChangesDbContext.SaveChanges();
            }


            EmployeeContractChanges eCC_NEW = new EmployeeContractChanges();

            ///2 
            eCC_NEW.LegalFormsID = lastCF != null ? lastCF.LegalFormsID : legalform.ID;
            eCC_NEW.StatusID = contract.StatusID == 0 ? 1 : contract.StatusID; //WILL BE PROVIDING STATUS
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
            eCC_NEW.DateCreated = DateTime.Now;


            EmpContractChangesDbContext.EmployeeContractChanges.Add(eCC_NEW);
            EmpContractChangesDbContext.SaveChanges();


        }

        public string StatusIs(int statusId)
        {
            AuthenticateContext db = EmpContractChangesDbContext;

            var result = db.FormStatuses.Where(x => x.ID == statusId).First();

            if (result != null)
            {
                return result.StatusName;
            }

            return "Not Found";
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


        public void ResetContractChange(int userID, EmployeeContractChanges contract)
        {//Will reset the user account information based on Last Change Log ID and earliest created entry with this
            //change log id.

            int lastChangeLogID = GetLCF(userID).ChangeLogID;

            AuthenticateContext db = EmpContractChangesDbContext;
            var result = db.EmployeeContractChanges
                .Where(x => x.ChangeLogID == lastChangeLogID)
                .OrderByDescending(x => x.ID).FirstOrDefault();

            Employee employeeToUpdate = EmpContractChangesDbContext.Employees.FirstOrDefault(x => x.ID == userID);

            employeeToUpdate.LastName = result.NewLastName;
            employeeToUpdate.Email = result.NewEmail;
            employeeToUpdate.Address = result.NewAddress;
            employeeToUpdate.City = result.NewCity;
            employeeToUpdate.State = result.NewState;
            employeeToUpdate.Zipcode = result.NewZipcode;
            employeeToUpdate.Country = result.NewCountry;
            employeeToUpdate.HomePhone = result.NewHomePhone;

            db.SaveChanges();


        }

        public EmployeeContractChanges GetEmployee(Employee employee)
        {
            return new EmployeeContractChanges()
            {
                StatusID = 1,
                EmployeeID = employee.ID,
                NewAddress = employee.Address,
                NewCity = employee.City,
                NewCountry = employee.Country,
                NewEmail = employee.Email,
                NewHomePhone = employee.HomePhone,
                NewLastName = employee.LastName,
                NewState = employee.State,
                NewZipcode = employee.Zipcode,
                DateCreated = DateTime.Now,
                ChangeLogID = 1

            };
        }

        public LegalForm CreateNewLegalForm(LegalForm lf) {
            LegalForm legalform = EmpContractChangesDbContext.LegalForms.Add(lf);
            EmpContractChangesDbContext.SaveChanges();

            return legalform;
        }

        public void CheckApproved(EmployeeContractChanges contract, int UserID)
        {
            AuthenticateContext db = EmpContractChangesDbContext;

            var status = db.FormStatuses.Where(x => x.ID == contract.StatusID).FirstOrDefault();

            if (status != null && status.StatusName.Contains("Approved"))
            {
                UpdateEmployee(new Employee
                {
                    ID = UserID,
                    LastName = contract.NewLastName,
                    Email = contract.NewEmail,
                    Address = contract.NewAddress,
                    City = contract.NewCity,
                    State = contract.NewState,
                    Zipcode = contract.NewZipcode,
                    Country = contract.NewCountry,
                    HomePhone = contract.NewHomePhone
                });
            }

        }
    }
}