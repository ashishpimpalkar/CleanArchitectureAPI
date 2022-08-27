using System;
using System.Collections.Generic;
using System.Text;
using Web.API.Core.RequestResponse.Request;
using Web.API.Core.RequestResponse.Response;

namespace Web.API.Core.Interfaces
{
    public interface IIntegrator
    {
        BaseResponse ExecuteRequest(BaseRequest request);
    }
}
