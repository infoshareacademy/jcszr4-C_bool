using AutoMapper;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;
using C_Bool.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace C_Bool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserReportsController : ControllerBase
    {
        private readonly IUserReportService _userReportService;
        private readonly IMapper _mapper;

        public UserReportsController(IUserReportService userReportService, IMapper mapper)
        {
            _userReportService = userReportService;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<UserReport> CreateUserReportEntry([FromBody] UserReportCreateDto userReportCreateDto)
        {

            var userReport = _mapper.Map<UserReport>(userReportCreateDto);
            _userReportService.CreateReportEntry(userReport);

            return CreatedAtAction(
                nameof(GetUserReportEntryByUserId),
                new { userId = userReport.UserId },
                userReport
            );
        }

        [HttpGet("{userId}")]
        public ActionResult<UserReport> GetUserReportEntryByUserId([FromRoute] int userId)
        {
            var userReport = _userReportService.GetReportEntryByUserId(userId);

            if (userReport == null)
            {
                return BadRequest($"Cannot get UserReport entry because UserId = {userId} not exists.");
            }

            return Ok(userReport);
        }

        [HttpPut("{userId}")]
        public ActionResult<UserReport> UpdateUserEntry([FromRoute] int userId, [FromBody] UserReportUpdateDto userReportUpdateDto)
        {
            var userReport = _userReportService.GetReportEntryByUserId(userId);
            userReport = _mapper.Map(userReport, userReport);

            if (userReport == null)
            {
                return BadRequest($"Cannot update UserReport entry because UserId = {userId} not exists.");
            }

            _userReportService.UpdateReportEntry(userReport);

            return NoContent();
        }
    }
}