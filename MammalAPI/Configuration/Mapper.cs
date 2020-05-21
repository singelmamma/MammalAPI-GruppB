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
            CreateMap<Mammal, MammalDTO>().ReverseMap();
            CreateMap<Habitat, HabitatDTO>().ReverseMap();
        }
    }
}
