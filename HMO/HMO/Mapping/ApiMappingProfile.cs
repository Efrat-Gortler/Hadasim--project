using AutoMapper;
using HMO.API.Models;
using HMO.Core.DTOs;
using HMO.Core.Entity;
using System;

namespace HMO.API.Mapping
{
    public class ApiMappingProfile:Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CityPostModel, City/*Dto*/>();
            //CreateMap<MemberPostModel, Member>();
            CreateMap<MemberPostModel, Member>()
               .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId));
            CreateMap<VaccinationPostModel, Vaccination>();
            CreateMap<ManufacturerPostModel, Manufacturer>();

        }

    }
}




