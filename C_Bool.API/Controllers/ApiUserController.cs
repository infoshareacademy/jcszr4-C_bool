using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_Bool.API.DAL.Entities;
using C_Bool.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace C_Bool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        private readonly IApiReportingUserService _apiReportingUserService;

        public ApiUserController(IApiReportingUserService apiReportingUserService)
        {
            _apiReportingUserService = apiReportingUserService;
        }

        // GET: api/<ApiUserController>
        [HttpGet("ListOfUsers")]
        public IEnumerable<User> GetUsers()
        {
            return _apiReportingUserService.GetUser();
        }
        [HttpGet("NumberOfActiveUsers")]
        public int GetNumberOfUser()
        {
            return _apiReportingUserService.NumberOfActiveUsers();
        }

        // POST api/<ApiUserController>
        [HttpPost("user")]
        public void Post([FromBody] User user)
        {
            _apiReportingUserService.Add(user);
        }
    }
}
