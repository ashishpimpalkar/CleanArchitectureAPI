using System;
using System.Collections.Generic;
using System.Text;

namespace Web.API.Core.RequestResponse.Request.User
{
    public class UserRequest:BaseRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
