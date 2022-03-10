using AutoMapper;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;
using C_Bool.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace C_Bool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGameTaskReportsController : ControllerBase
    {
        private readonly IUserGameTaskReportService _userGameTaskReportService;
        private readonly IMapper _mapper;

        public UserGameTaskReportsController(IUserGameTaskReportService userGameTaskReportService, IMapper mapper)
        {
            _userGameTaskReportService = userGameTaskReportService;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<UserGameTaskReport> AddUserGameTask([FromBody] UserGameTaskReportCreateDto userGameTaskReportCreateDto)
        {
            var userGameTaskReport = _mapper.Map<UserGameTaskReport>(userGameTaskReportCreateDto);
            _userGameTaskReportService.AddUserGameTask(userGameTaskReport);

            return Ok(); //Created($"reports/{userGameTask.Id}", userGameTask);
        }

    }
}
