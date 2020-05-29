using System.Collections.Generic;

namespace MammalAPI.DTO
{
    public class HabitatDTO
    {
        public int HabitatID { get; set; }
        public string Name { get; set; }
        public IList<MammalDTO> Mammal { get; set; }
    }
}
