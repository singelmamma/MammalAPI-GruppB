using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MammalAPI.Models
{
    public class Mammal
    {
        public int MammalID { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public string LatinName { get; set; }
        public int Lifespan { get; set; }
        public int FmailyID { get; set; }

    }
}
