using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_Bool.API.DAL.Entities;
using C_Bool.API.Enums;
using C_Bool.API.Services;
using MoreLinq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace C_Bool.API.Controllers
{
    //TODO: Zmienić IEnumerable na ActionResult
    //TODO: zmienić "seats" na "count"

    //glowny:4455/api/places?fromDate=455454&thru
    //glowny:4455/api/places/1


    [Route("api/[controller]")]
    [ApiController]
    public class ApiGameTaskController : ControllerBase
    {
        private readonly IApReportingGameTaskService _apiReportService;

        public ApiGameTaskController(IApReportingGameTaskService apiReportService)
        {
            _apiReportService = apiReportService;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAllGameTask")]
        public IEnumerable<GameTask> GetGameTasks()
        {
            return _apiReportService.GetGameTasks();
        }
        [HttpGet("GetTheMostPopularGameTaskType")]
        public string MostPopularGameTask()
        {
            return _apiReportService.TheMostPopularTypeOfTask();
        }
        
        [HttpGet("Top {seats} Places with the most tasks")]
        public IEnumerable<KeyValuePair<int, int>> TopXPlaceWithTasks(int seats)
        {
            return _apiReportService.TopListPlacesWithTheMostTask(seats);
        }
        [HttpGet("Top {seats} Test")]
        public IEnumerable<KeyValuePair<int, int>> TopXTest(int seats)
        {
            return _apiReportService.Test(seats);
        }

        // POST api/<ValuesController>
        [HttpPost("gameTask")]
        public void Post([FromBody] GameTask gameTask)
        {
            _apiReportService.Add(gameTask);
        }
    }
}
