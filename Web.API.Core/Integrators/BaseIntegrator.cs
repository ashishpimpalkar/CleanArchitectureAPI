using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Web.API.Core.RequestResponse.Request;
using Web.API.Core.RequestResponse.Response;

namespace Web.API.Core.Integrators
{
    public class BaseIntegrator
    {
        public readonly ILogger<BaseIntegrator> Logger;

        public BaseIntegrator(ILogger<BaseIntegrator> logger)
        {
            Logger = logger;
        }

        public BaseResponse ExecuteRequest(BaseRequest request)
        {
            try
            {
                return (BaseResponse)this.GetType().InvokeMember("DelegateRequest",
                    System.Reflection.BindingFlags.DeclaredOnly |
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic
                    | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod,
                    null,
                    this,
                    new object[] { request });

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
