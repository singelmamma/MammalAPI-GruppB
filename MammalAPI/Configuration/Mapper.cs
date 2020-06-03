using System.Linq;
using AutoMapper;
using MammalAPI.DTO;
using MammalAPI.Models;

namespace MammalAPI.Configuration
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Mammal, MammalDTO>().ForMember(dto => dto.Habitats, opt => opt.MapFrom(x => x.MammalHabitats
                    .Select(y => new Habitat { HabitatID = y.Habitat.HabitatID, Name = y.Habitat.Name, MammalHabitats = null }).ToList())).ReverseMap();
            
            CreateMap<Family, FamilyDTO>().ForMember(dto => dto.Mammals, opt => opt.MapFrom(x => x.Mammals
                        .Select(y => new Mammal {MammalId = y.MammalId, Name = y.Name,
                        MammalHabitats = null}).ToList())).ReverseMap() ;

            CreateMap<Habitat, HabitatDTO>().ForMember(dto => dto.Mammal, opt => opt.MapFrom(x => x.MammalHabitats
                   .Select(y => new Mammal { 
                           MammalId = y.Mammal.MammalId,
                           Name = y.Mammal.Name,
                           Length = y.Mammal.Length,
                           Weight = y.Mammal.Weight,
                           Family = y.Mammal.Family,
                           LatinName = y.Mammal.LatinName,
                           Lifespan = y.Mammal.Lifespan,
                           MammalHabitats = null
                   }).ToList())).ReverseMap();
        }
    }
}
