using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Models.DTO.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityAPICreate>().ReverseMap();
            CreateMap<City, CityAPIUpdate>().ReverseMap();
        }
    }
}
