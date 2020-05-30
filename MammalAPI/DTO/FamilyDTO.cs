using System.Collections.Generic;

namespace MammalAPI.DTO
{
    public class FamilyDTO
    {
        public int FamilyID { get; set; }
        public string Name { get; set; }
        public ICollection<MammalDTO> Mammals { get; set; }
    }
}
