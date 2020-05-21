using System.Collections.Generic;
using MammalAPI.Models;

namespace MammalAPI.DTO
{
    public class HabitatDTO
    {
        public int HabitatID { get; set; }
        public string Name { get; set; }
        public IList<Habitats> MammalHabitats { get; set; }
    }
}
