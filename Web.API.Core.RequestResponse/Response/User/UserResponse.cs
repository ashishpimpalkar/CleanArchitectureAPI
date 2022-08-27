using System;
using System.Collections.Generic;
using System.Text;
using Web.API.Core.RequestResponse.DTOs;

namespace Web.API.Core.RequestResponse.Response.User
{
    public class UserResponse:BaseResponse
    {
       public string Token { get; set; } 
    }
}
