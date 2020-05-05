using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MammalAPI.Models
{
    public class Mammal
    {
        [Key]
        public int MammalId { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public string LatinName { get; set; }
        public int Lifespan { get; set; }
        public int FamilyId { get; set; }

        public IList<MammalHabitat> MammalHabitats { get; set; }
    }
}
