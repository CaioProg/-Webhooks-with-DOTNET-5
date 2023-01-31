using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;

namespace AirlineWeb.Profiles
{
    public class FlightDetailProfile : Profile
    {   
        public FlightDetailProfile()
        {
            // Source -> Target
            CreateMap<FlightDetail, FlightDetailReadDto>();
            CreateMap<FlightDetailCreateDto, FlightDetail>();
            CreateMap<FlightDetailUpdateDto, FlightDetail>();
        }

    }
}