using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Configuration
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Mammal, MammalDTO>().ForMember(dto => dto.Habitats, opt => opt.MapFrom(x => x.MammalHabitats.Select(y => y.Habitat).ToList())).ReverseMap();
            CreateMap<Habitat, HabitatDTO>()
            .ForMember(dto => dto.Mammal, opt => opt.MapFrom(x => x.MammalHabitats.Select(y => y.Mammal).ToList()));
            CreateMap<Family, FamilyDTO>().ReverseMap();
        }
    }
}
