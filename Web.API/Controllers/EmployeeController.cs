using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.API.Core.Interfaces;

using Web.API.Core.RequestResponse;
using Web.API.Core.RequestResponse.Request;
using Web.API.Core.RequestResponse.Response;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IIntegrator _integrator { get; set; }
        private readonly ILogger _logger;

        public EmployeeController([KeyFilter("Employee")]IIntegrator iIntegrator,ILogger<EmployeeController> logger)
        {
            _integrator = iIntegrator;
            _logger = logger;
            
        }

        [HttpGet]        
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.LogInformation("EmployeeController --> Get--> Method Start");

            var request = new EmployeeRequest {OperationType=CommonHelper.OperationType.None };
            BaseResponse response = _integrator.ExecuteRequest(request);

            _logger.LogInformation("EmployeeController --> Get--> Method End");

            return new String[] { "String1", "String2" };

        }

        [HttpGet]
        [Route("AllEmployeeDetails")]
        public ActionResult<BaseResponse> GetEmployeeDetails()
        {
            _logger.LogInformation("EmployeeController --> AllEmployeeDetails--> Method Start");

            var request = new EmployeeRequest { OperationType=CommonHelper.OperationType.SelectAll };
            
            BaseResponse response = _integrator.ExecuteRequest(request);


            _logger.LogInformation("EmployeeController --> AllEmployeeDetails--> Method End");
            return response;
        }
        [HttpGet]
        [Route("GetEmployeeDetailsById/{employeeId}")]
        public ActionResult<BaseResponse> GetEmployeeById(int employeeId)
        {
            _logger.LogInformation("EmployeeController --> GetEmployeeDetailsById--> Method Start");

            var request = new EmployeeRequest { OperationType = CommonHelper.OperationType.SelectById,EmployeeId=employeeId};
            BaseResponse response = _integrator.ExecuteRequest(request);

            _logger.LogInformation("EmployeeController --> GetEmployeeDetailsById--> Method End");
            return response;
        }

        [HttpPost]
        [Route("InsertEmployeeDetails")]
        public ActionResult<BaseResponse> AddEmployee(EmployeeRequest request)
        {
            request.OperationType = CommonHelper.OperationType.AddOne;
            BaseResponse response = _integrator.ExecuteRequest(request);
            return response;
        }

        [HttpPost]
        [Route("UpdateEmployeeDetails")]
        public ActionResult<BaseResponse> UpdateEmployee(EmployeeRequest request)
        {
            request.OperationType = CommonHelper.OperationType.UpdateOne;
            BaseResponse response = _integrator.ExecuteRequest(request);
            return response;
        }

        [HttpGet]
        [Route("DeleteEmployeeDetails/{employeeId}")]
        public ActionResult<BaseResponse> UpdateEmployee(int employeeId)
        {
            var request = new EmployeeRequest{OperationType= CommonHelper.OperationType.DeleteOne,EmployeeId= employeeId };
            BaseResponse response = _integrator.ExecuteRequest(request);
            return response;
        }
    }
}