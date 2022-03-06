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
        public ActionResult<UserGameTaskReport> AddUserGameTask([FromBody] UserGameTaskWriteDto userGameTaskWriteDto)
        {
            var userGameTask = _mapper.Map<UserGameTaskReport>(userGameTaskWriteDto);
            _userGameTaskReportService.AddUserGameTask(userGameTask);

            return Created($"reports/{userGameTask.Id}", userGameTask);
        }

    }
}
