using System;
using System.Collections.Generic;
using System.Text;

namespace Web.API.Core.Entities
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
