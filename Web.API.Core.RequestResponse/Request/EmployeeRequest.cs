using System;
using System.Collections.Generic;
using System.Text;
using Web.API.Core.RequestResponse.DTOs;

namespace Web.API.Core.RequestResponse.Request
{
    public class EmployeeRequest:BaseRequest
    {
        public int EmployeeId { get; set; }
        public EmployeeDTO employeeDTO { get; set; }
    }
}
