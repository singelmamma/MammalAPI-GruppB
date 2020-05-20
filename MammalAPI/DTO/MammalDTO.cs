using System.Collections.Generic;
using MammalAPI.Models;

namespace MammalAPI.DTO
{
    public class MammalDTO : HateoasDTOBase
    {
        public int MammalID { get; set; }
        public string Name { get; set; }
        public int Children { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public string LatinName { get; set; }
        public int Lifespan { get; set; }
        public List<Habitat> Habitats { get; set; }
        public Family Family { get; set; }
    }
}
