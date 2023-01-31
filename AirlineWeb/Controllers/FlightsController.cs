using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;

        public FlightsController(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{flightCode}", Name = "GetFlightDetailsByCode")]
        public ActionResult<FlightDetailReadDto> GetFlightDetailsByCode(string flightCode)
        {
            var flight = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightCode);

            if(flight == null)
            {
                return NotFound();
            }

            //This mapping won't work as I have not done the Profiles section Duh!!!
            return Ok(_mapper.Map<FlightDetailReadDto>(flight));
        }

        [HttpPost]
        public ActionResult<FlightDetailReadDto> CreateFlight(FlightDetailCreateDto flightDetailCreateDto)
        {
            var flight = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightDetailCreateDto.FlightCode);

            if (flight == null)
            {
                var FlightDetailModel = _mapper.Map<FlightDetail>(flightDetailCreateDto);

                try
                {
                    _context.FlightDetails.Add(FlightDetailModel);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                var flightDetailReadDto = _mapper.Map<FlightDetailReadDto>(FlightDetailModel);
                return CreatedAtRoute(nameof(GetFlightDetailsByCode), new {flightCode = flightDetailReadDto.FlightCode}, flightDetailReadDto);
            }
            else
            {
                return NoContent();
            }
        }
    }
}