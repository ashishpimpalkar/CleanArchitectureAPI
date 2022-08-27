//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Web.API.Core.Entities;
//using Web.API.Core.Gateways;
//using Web.API.Core.Interfaces;
//using Web.API.Core.RequestResponse.Request.User;
//using Web.API.Core.RequestResponse.Response;
//using Web.API.Core.RequestResponse.Response.User;

//namespace Web.API.Core.Integrators
//{
//    class AuthIntegrator : BaseIntegrator, IIntegrator
//    {
//        #region Repositories
//        public AuthIntegrator(ILogger<BaseIntegrator> logger) : base(logger)
//        {

//        }
//        public Lazy<IGenericRepository<Employee>> EmployeeRepository { get; set; }
//        #endregion


//        #region DelegateRequest
//        private UserResponse DelegateRequest(UserRequest request)
//        {
//            UserResponse response = new UserResponse();
//            switch (request.OperationType)
//            {
//                case RequestResponse.CommonHelper.OperationType.None:
//                    break;             
//                case RequestResponse.CommonHelper.OperationType.GetById:
//                    break;
//                default:
//                    break;
//            }

//            return response;
//        }

//        #endregion

//        #region PrivateMethod

//        public Employee AuthenticateUser(UserRequest request)
//        {
//            var employee = EmployeeRepository.Value.FindBy(x => x.Use);
//            return employee;
//        }

//        private string GenerateJSONWebToken(Employee employeeInfo)
//        {
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var claims = new[] {
//                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
//                new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
//                new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            };

//            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
//                _config["Jwt:Issuer"],
//                claims,
//                expires: DateTime.Now.AddMinutes(120),
//                signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//        #endregion
//    }
//}
