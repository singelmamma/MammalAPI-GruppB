using MammalAPI.HATEOAS;
using System.Collections.Generic;

namespace MammalAPI.DTO
{
    public class HabitatDTO : HateoasLinkBase
    {
        public int HabitatID { get; set; }
        public string Name { get; set; }
        public IList<MammalDTO> Mammal { get; set; }
    }
}
