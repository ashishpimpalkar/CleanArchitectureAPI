using Autofac;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Web.API.Core.Integrators;
using Web.API.Core.Interfaces;
using Web.API.Core.RequestResponse;
using Web.API.Core.RequestResponse.Request;
using Web.API.Core.RequestResponse.Response;
using Xunit;

namespace Web.API.Core.Test.Integrator
{
    public class EmployeeIntegratoTest
    {
        private readonly IIntegrator _iintegrator;
        EmployeeResponse responseEmployee;

        public EmployeeIntegratoTest()
        {
            BootStrap.Initialize();
            _iintegrator = BootStrap.Container.ResolveNamed<IIntegrator>("Employee", new NamedParameter("logger", new NullLogger<BaseIntegrator>()));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetEmployeeById(int employeeId)
        {
            var response = _iintegrator.ExecuteRequest(new EmployeeRequest
            {
                OperationType=CommonHelper.OperationType.SelectById,
                EmployeeId = employeeId

            });

            responseEmployee = (EmployeeResponse)response;
            Assert.IsType<EmployeeResponse>(response);
            Assert.True(response.IsSucess);
        }


    }
}
