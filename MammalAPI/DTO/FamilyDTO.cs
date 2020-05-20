using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MammalAPI.DTO
{
    public class FamilyDTO
    {
        public int FamilyID { get; set; }
        public string Name { get; set; }
        public ICollection<MammalDTO> Mammals { get; set; }
    }
}
