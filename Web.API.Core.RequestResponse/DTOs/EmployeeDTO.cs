using System;
using System.Collections.Generic;
using System.Text;

namespace Web.API.Core.RequestResponse.DTOs
{
    public class EmployeeDTO
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
    }
}
