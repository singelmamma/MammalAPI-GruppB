using MammalAPI.HATEOAS;
using System.Collections.Generic;

namespace MammalAPI.DTO
{
    public class FamilyDTO : HateoasLinkBase
    {
        public int FamilyID { get; set; }
        public string Name { get; set; }
        public ICollection<MammalDTO> Mammals { get; set; }
    }
}
