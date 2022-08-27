using System;
using System.Collections.Generic;
using System.Text;
using Web.API.Core.Entities;
using Web.API.Core.Gateways;
using Web.API.Core.Interfaces;
using Web.API.Core.RequestResponse.Request;
using Web.API.Core.RequestResponse.Response;
using System.Linq;
using Web.API.Core.RequestResponse.DTOs;
using Microsoft.Extensions.Logging;

namespace Web.API.Core.Integrators
{
    public class EmployeeIntegrator : BaseIntegrator, IIntegrator
    {
        #region Repositories
        public EmployeeIntegrator(ILogger<BaseIntegrator> logger) : base(logger)
        {

        }
        public Lazy<IGenericRepository<Employee>> EmployeeRepository { get; set; }

        #endregion

        #region DelegateRequest

        private EmployeeResponse DelegateRequest(EmployeeRequest request)
        {
            Logger.LogInformation(string.Format("DelegateRequest(EmployeeRequest) --> Start with {0}", Convert.ToString(request)));

            EmployeeResponse response = new EmployeeResponse();

            switch (request.OperationType)
            {
                case RequestResponse.CommonHelper.OperationType.None:
                    break;
                case RequestResponse.CommonHelper.OperationType.SelectById:
                    Logger.LogInformation("DelegateRequest(EmployeeRequest)--> SelectById");
                    response = GetEmployeeById(request);
                    break;
                case RequestResponse.CommonHelper.OperationType.SelectAll:
                    response = GetEmployeeDetails(request);
                    break;
                case RequestResponse.CommonHelper.OperationType.AddOne:
                    response = AddEmployee(request);
                    break;
                case RequestResponse.CommonHelper.OperationType.AddMany:
                    break;
                case RequestResponse.CommonHelper.OperationType.UpdateOne:
                    response = UpdateEmployee(request);
                    break;
                case RequestResponse.CommonHelper.OperationType.UpdateAll:
                    break;
                case RequestResponse.CommonHelper.OperationType.SelectSingle:
                    break;
                case RequestResponse.CommonHelper.OperationType.DeleteAll:
                    break;
                case RequestResponse.CommonHelper.OperationType.DeleteOne:
                    response = DeleteEmployee(request);
                    break;
                case RequestResponse.CommonHelper.OperationType.GetAll:
                    break;
                case RequestResponse.CommonHelper.OperationType.GetById:
                    break;
                default:
                    break;
            }

            Logger.LogInformation(string.Format("DelegateRequest(EmployeeRequest) --> End with {0}", response.ToString()));
            return response;

        }
        #endregion

        #region PrivateMethods

        private EmployeeResponse GetEmployeeDetails(EmployeeRequest request)
        {
            Logger.LogInformation("DelegateRequest(EmployeeRequest)-->GetEmployeeDetails--> Method Start");

            EmployeeResponse response = new EmployeeResponse();
            response.employeeDTOs = EmployeeRepository.Value.GetAll()
                                   .Select(t => new EmployeeDTO
                                   {
                                       EmpId = t.EmpId,
                                       EmpName = t.EmpName,
                                       DateOfBirth = (DateTime)t.DateOfBirth,
                                       EmailId = t.EmailId,
                                       Gender = t.Gender,
                                       Address = t.Address,
                                       Pincode = t.Pincode
                                   }).ToList();

            response.IsSucess = true;

            Logger.LogInformation("DelegateRequest(EmployeeRequest)-->GetEmployeeDetails--> Method End");
            return response;
        }

        public EmployeeResponse GetEmployeeById(EmployeeRequest request)
        {
            EmployeeResponse response = new EmployeeResponse();

            response.EmployeeDTO = EmployeeRepository.Value.FindBy(a => a.EmpId == request.EmployeeId)
                                     .Select(t => new EmployeeDTO
                                     {
                                         EmpId = t.EmpId,
                                         EmpName = t.EmpName,
                                         DateOfBirth = (DateTime)t.DateOfBirth,
                                         EmailId = t.EmailId,
                                         Gender = t.Gender,
                                         Address = t.Address,
                                         Pincode = t.Pincode
                                     })
                                     .FirstOrDefault();

            if (response.EmployeeDTO != null) response.IsSucess = true;
            return response;

        }

        public EmployeeResponse AddEmployee(EmployeeRequest request)
        {
            EmployeeResponse response = new EmployeeResponse();
            Employee employee = null;

            if (request.employeeDTO != null)
            {
                employee = new Employee();
                employee.EmpName = request.employeeDTO.EmpName;
                employee.EmailId = request.employeeDTO.EmailId;
                employee.DateOfBirth = request.employeeDTO.DateOfBirth;
                employee.Gender = request.employeeDTO.Gender;
                employee.Address = request.employeeDTO.Address;
                employee.Pincode = request.employeeDTO.Pincode;

                try
                {
                    EmployeeRepository.Value.Add(employee);
                    EmployeeRepository.Value.Save();
                    response.IsSucess = true;
                }
                catch (Exception)
                {

                    throw;
                }

            }


            
            return response;

        }

        public EmployeeResponse UpdateEmployee(EmployeeRequest request)
        {
            EmployeeResponse response = new EmployeeResponse();

            var employee = EmployeeRepository.Value.FindBy(a => a.EmpId == request.employeeDTO.EmpId).FirstOrDefault();

            employee.EmpName = request.employeeDTO.EmpName;
            employee.DateOfBirth = request.employeeDTO.DateOfBirth;
            employee.EmailId = request.employeeDTO.EmailId;
            employee.Gender = request.employeeDTO.Gender;
            employee.Address = request.employeeDTO.Address;
            employee.Pincode = request.employeeDTO.Pincode;

            try
            {
                EmployeeRepository.Value.Edit(employee);
                EmployeeRepository.Value.Save();
                response.IsSucess = true;
            }
            catch (Exception)
            {

                throw;
            }


            return response;
        }
        public EmployeeResponse DeleteEmployee(EmployeeRequest request)
        {
            EmployeeResponse response = new EmployeeResponse();

            var employee = EmployeeRepository.Value.FindBy(a => a.EmpId == request.EmployeeId).FirstOrDefault();

            if (employee != null)
            {
                EmployeeRepository.Value.Delete(employee);
                EmployeeRepository.Value.Save();
            }

            return response;
        }

        #endregion
    }
}
