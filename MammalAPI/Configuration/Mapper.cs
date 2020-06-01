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
                    .Select(y => y.Habitat).ToList())).ReverseMap();
            CreateMap<Family, FamilyDTO>().ReverseMap();

            CreateMap<Habitat, HabitatDTO>().ForMember(dto => dto.Mammal, opt => opt.MapFrom(x => x.MammalHabitats
                   .Select(y => y.Mammal).ToList())).ReverseMap();
        }
    }
}
