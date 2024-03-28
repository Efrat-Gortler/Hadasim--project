using AutoMapper;
using HMO.Core.DTOs;
using HMO.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMO.Core.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<Vaccination, VaccinationDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Manufacturer, ManufacturerDto>().ReverseMap();
        }
    }
}
