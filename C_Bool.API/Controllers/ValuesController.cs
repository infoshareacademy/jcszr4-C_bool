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
    public class ValuesController : ControllerBase
    {
        private readonly IApiReportService _apiReportService;

        public ValuesController(IApiReportService apiReportService)
        {
            _apiReportService = apiReportService;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAllPlaces")]
        public IEnumerable<PlaceReport> Get()
        {
            return _apiReportService.GetPlaces();
        }
        [HttpGet("ListOfUsers")]
        public IEnumerable<UserReport> GetUsers()
        {
            return _apiReportService.GetUser();
        }
        [HttpGet("NumberOfActiveUsers")]
        public int GetNumberOfUser()
        {
            return _apiReportService.NumberOfActiveUsers();
        }
        [HttpGet("GetAllGameTask")]
        public IEnumerable<GameTaskReport> GetGameTasks()
        {
            return _apiReportService.GetGameTasks();
        }
        [HttpGet("GetTheMostPopularGameTaskType")]
        public string MostPopularGameTask()
        {
            return _apiReportService.TheMostPopularTypeOfTask();
        }
        [HttpGet("Top {seats} Places")]
        public IEnumerable<string> TopListPlace(int seats)
        {
            return _apiReportService.TopListPlaces(seats);
        }

        [HttpGet("Top {seats} Places with the most tasks")]
        public IEnumerable<KeyValuePair<int, int>> TopXPlaceWithTasks(int seats)
        {
            return _apiReportService.TopListPlacesWithTheMostTask(seats);
        }

        // GET api/<ValuesController>/5
        [HttpGet("DisplayPlacesBySelected{id}")]
        public ActionResult<PlaceReport> Get(int id)
        {
            var place = _apiReportService.GetPlace(id);
            return Ok(place);
        }

        // POST api/<ValuesController>
        [HttpPost("placeReport")]
        public void Post([FromBody] PlaceReport placeReport)
        {
            _apiReportService.AddPlace(placeReport);
        }
        [HttpPost("userReport")]
        public void Post([FromBody] UserReport userReport)
        {
            _apiReportService.AddUser(userReport);
        }
        [HttpPost("gameTaskReport")]
        public void Post([FromBody] GameTaskReport gameTaskReport)
        {
            _apiReportService.AddGameTask(gameTaskReport);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
