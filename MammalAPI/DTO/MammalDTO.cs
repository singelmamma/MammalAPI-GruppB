using System.Collections.Generic;

namespace MammalAPI.DTO
{
    public class MammalDTO
    {
        public int MammalID { get; set; }
        public string Name { get; set; }
        public int Children { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public string LatinName { get; set; }
        public int Lifespan { get; set; }

        // Unsure what we really want below...need to check with team if the corect type is being used here by me.
        public List<int> HabitatNames { get; set; }
        public List<int> FamilyNames { get; set; }
    }
}
