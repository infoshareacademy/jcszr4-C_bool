using System;
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
        public ActionResult<UserGameTaskReport> CreateUserGameTaskReportEntry([FromBody] UserGameTaskReportCreateDto userGameTaskReportCreateDto)
        {

            var userGameTaskReport = _mapper.Map<UserGameTaskReport>(userGameTaskReportCreateDto);
            _userGameTaskReportService.CreateReportEntry(userGameTaskReport);

            return CreatedAtAction(
                nameof(GetUserGameTaskReportEntryByUserGameTaskId),
                new {userGameTaskId = userGameTaskReport.UserGameTaskId},
                userGameTaskReport
            );
        }

        [HttpGet("{userGameTaskId}")]
        public ActionResult<UserGameTaskReport> GetUserGameTaskReportEntryByUserGameTaskId([FromRoute] int userGameTaskId)
        {
            var userGameTaskReport = _userGameTaskReportService.GetReportEntryByUserGameTaskId(userGameTaskId);

            if (userGameTaskReport == null)
            {
                return BadRequest($"Cannot get UserGameTaskReport entry because UserGameTaskId = {userGameTaskId} not exists.");
            }

            return Ok(userGameTaskReport);
        }

        [HttpPut ("{userGameTaskId}")]
        public ActionResult<UserGameTaskReport> UpdateUserGameTaskReportEntry([FromRoute] int userGameTaskId, [FromBody] UserGameTaskReportUpdateDto userGameTaskReportUpdateDto)
        {
            var userGameTaskReport = _userGameTaskReportService.GetReportEntryByUserGameTaskId(userGameTaskId);
            userGameTaskReport = _mapper.Map(userGameTaskReportUpdateDto, userGameTaskReport);

            if (userGameTaskReport == null)
            {
                return BadRequest($"Cannot update UserGameTaskReport entry because UserGameTaskId = {userGameTaskId} not exists.");
            }

            _userGameTaskReportService.UpdateReportEntry(userGameTaskReport);

            return NoContent();
        }
        [HttpGet("mostPopularByUsers")]
        public ActionResult<GetCountByDto> GetGameTasksWithHighestUsersCount([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo, [FromQuery] int limit)
        {
            var mostPopularGameTaskByUsers = _userGameTaskReportService.GetMostPopularByUsers(dateFrom, dateTo, limit);

            return Ok(mostPopularGameTaskByUsers);
        }

        [HttpGet("mostActiveUsers")]
        public ActionResult<GetCountByDto> GetMostActiveUsers([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo, [FromQuery] int limit)
        {
            var mostActiveUsers = _userGameTaskReportService.GetMostActiveUsers(dateFrom, dateTo, limit);

            return Ok(mostActiveUsers);
        }

        [HttpGet("doneCount")]
        public ActionResult<GetPartialCountDto> GetDoneGameTaskCount()
        {
            var doneGameTaskCount = _userGameTaskReportService.GetDoneGameTaskCount();

            return Ok(doneGameTaskCount);
        }

        [HttpGet("averageDoneTime")]
        public ActionResult<GetPartialCountDto> GetAverageGameTaskDoneTime()
        {
            var averageDoneTime = _userGameTaskReportService.GetAverageTimeBetweenCreatingAndDone();

            return Ok(averageDoneTime);
        }
    }
}
