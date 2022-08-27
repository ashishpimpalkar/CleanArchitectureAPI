using System;
using System.Collections.Generic;
using System.Text;
using Web.API.Core.RequestResponse.DTOs;

namespace Web.API.Core.RequestResponse.Response
{
    public class EmployeeResponse:BaseResponse
    {
        public List<EmployeeDTO> employeeDTOs { get; set; }  
        public EmployeeDTO EmployeeDTO { get; set; }
    }
}
