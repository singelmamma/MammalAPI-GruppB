using System.Collections.Generic;
using MammalAPI.HATEOAS;

namespace MammalAPI.DTO
{
    public class MammalDTO : HateoasLinkBase
    {
        public int MammalID { get; set; }
        public string Name { get; set; }
        public int Children { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public string LatinName { get; set; }
        public int Lifespan { get; set; }
        public ICollection<HabitatDTO> Habitats { get; set; }
        public FamilyDTO Family { get; set; }
    }
}
