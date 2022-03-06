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
    public class ApiPlaceController : ControllerBase
    {
        private readonly IApiReportingPlaceService _apiReportingPlaceService;

        public ApiPlaceController(IApiReportingPlaceService apiReportingPlaceService)
        {
            _apiReportingPlaceService = apiReportingPlaceService;
        }

        // GET: api/<ApiPlaceController>
        [HttpGet("GetAllPlaces")]
        public IEnumerable<Place> Get()
        {
            return _apiReportingPlaceService.GetPlaces();
        }

        // GET api/<ApiPlaceController>/5
        [HttpGet("Top {seats} Places")]
        public IEnumerable<string> TopListPlace(int seats)
        {
            return _apiReportingPlaceService.TopListPlaces(seats);
        }
        [HttpGet("DisplayPlacesBySelected{id}")]
        public ActionResult<Place> Get(int id)
        {
            var place = _apiReportingPlaceService.GetPlace(id);
            return Ok(place);
        }

        // POST api/<ApiPlaceController>
        [HttpPost("place")]
        public void Post([FromBody] Place place)
        {
            _apiReportingPlaceService.Add(place);
        }
    }
}
