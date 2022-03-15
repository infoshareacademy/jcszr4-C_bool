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
    public class PlaceReportsController : ControllerBase
    {
        private readonly IPlaceReportService _placeReportService;
        private readonly IMapper _mapper;

        public PlaceReportsController(IPlaceReportService placeReportService, IMapper mapper)
        {
            _placeReportService = placeReportService;
            _mapper = mapper;
        }
        // GET api/<ApiPlaceController>/5
        [HttpGet("/Top{x}Places")]
        public IEnumerable<string> TopListPlace(int x)
        {
            return _placeReportService.TopListPlaces(x);
        }
        [HttpPost]
        public ActionResult<PlaceReport> CreatePlaceReportEntry([FromBody] PlaceReportCreateDto placeReportCreateDto)
        {

            var placeReport = _mapper.Map<PlaceReport>(placeReportCreateDto);
            _placeReportService.CreateReportEntry(placeReport);

            return CreatedAtAction(
                nameof(GetPlaceReportEntryByPlaceId),
                new { placeId = placeReport.PlaceId },
                placeReport
            );
        }

        [HttpGet("{placeId}")]
        public ActionResult<PlaceReport> GetPlaceReportEntryByPlaceId([FromRoute] int placeId)
        {
            var placeReport = _placeReportService.GetReportEntryByPlaceId(placeId);

            if (placeReport == null)
            {
                return BadRequest($"Cannot get PlaceReport entry because PlaceId = {placeId} not exists.");
            }

            return Ok(placeReport);
        }

        [HttpPut("{placeId}")]
        public ActionResult<PlaceReport> UpdatePlaceEntry([FromRoute] int placeId, [FromBody] PlaceReportUpdateDto placeReportUpdateDto)
        {
            var placeReport = _placeReportService.GetReportEntryByPlaceId(placeId);
            placeReport = _mapper.Map(placeReport, placeReport);

            if (placeReport == null)
            {
                return BadRequest($"Cannot update PlaceReport entry because PlaceId = {placeId} not exists.");
            }

            _placeReportService.UpdateReportEntry(placeReport);

            return NoContent();
        }
    }
}