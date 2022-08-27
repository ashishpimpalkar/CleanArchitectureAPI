using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.API.Core.Entities;
using Web.API.Core.Interfaces;
using Web.API.Core.RequestResponse;
using Web.API.Core.RequestResponse.Request;
using Web.API.Core.RequestResponse.Response;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AuthController : ControllerBase
    {
        IIntegrator _integrator { get; set; }
        private readonly ILogger _logger;

        public AuthController([KeyFilter("Auth")]IIntegrator iIntegrator, ILogger<AuthController> logger)
        {
            _integrator = iIntegrator;
            _logger = logger;

        }


        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.LogInformation("EmployeeController --> Get--> Method Start");

            var request = new EmployeeRequest { OperationType = CommonHelper.OperationType.None };
            BaseResponse response = _integrator.ExecuteRequest(request);

            _logger.LogInformation("EmployeeController --> Get--> Method End");

            return new String[] { "String1", "String2" };

        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public ActionResult<BaseResponse> Login(EmployeeRequest request)
        {
            request.OperationType = CommonHelper.OperationType.GetById;
            BaseResponse response = _integrator.ExecuteRequest(request);
            return response;
        }
    }
}