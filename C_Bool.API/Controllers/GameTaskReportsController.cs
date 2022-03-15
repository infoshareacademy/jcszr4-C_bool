using System.Collections.Generic;
using AutoMapper;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;
using C_Bool.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace C_Bool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameTaskReportsController : ControllerBase
    {
        private readonly IGameTaskReportService _gameTaskReportService;
        private readonly IMapper _mapper;

        public GameTaskReportsController(IGameTaskReportService gameTaskReportService, IMapper mapper)
        {
            _gameTaskReportService = gameTaskReportService;
            _mapper = mapper;
        }
        [HttpGet("/PopularGameTask")]
        public string MostPopularGameTask()
        {
            return _gameTaskReportService.TheMostPopularTypeOfTask();
        }

        [HttpGet("top")]
        public IEnumerable<KeyValuePair<int, int>> TopXPlaceWithTasks([FromQuery] int x)
        {
            return _gameTaskReportService.TopListPlacesWithTheMostTask(x);
        }

        [HttpPost]
        public ActionResult<GameTaskReport> CreateUserGameTaskReportEntry([FromBody] GameTaskReportCreateDto gameTaskReportCreateDto)
        {

            var gameTaskReport = _mapper.Map<GameTaskReport>(gameTaskReportCreateDto);
            _gameTaskReportService.CreateReportEntry(gameTaskReport);

            return CreatedAtAction(
                nameof(GetGameTaskReportEntryByGameTaskId),
                new { gameTaskId = gameTaskReport.GameTaskId },
                gameTaskReport
            );
        }

        [HttpGet("{gameTaskId}")]
        public ActionResult<GameTaskReport> GetGameTaskReportEntryByGameTaskId([FromRoute] int gameTaskId)
        {
            var gameTaskReport = _gameTaskReportService.GetReportEntryByGameTaskId(gameTaskId);

            if (gameTaskReport == null)
            {
                return BadRequest($"Cannot get GameTaskReport entry because GameTaskId = {gameTaskId} not exists.");
            }

            return Ok(gameTaskReport);
        }

        [HttpPut("{gameTaskId}")]
        public ActionResult<GameTaskReport> UpdateGameTaskReportEntry([FromRoute] int gameTaskId, [FromBody] GameTaskReportUpdateDto gameTaskReportUpdateDto)
        {
            var gameTaskReport = _gameTaskReportService.GetReportEntryByGameTaskId(gameTaskId);
            gameTaskReport = _mapper.Map(gameTaskReportUpdateDto, gameTaskReport);

            if (gameTaskReport == null)
            {
                return BadRequest($"Cannot update GameTaskReport entry because GameTaskId = {gameTaskId} not exists.");
            }

            _gameTaskReportService.UpdateReportEntry(gameTaskReport);

            return NoContent();
        }
    }
}