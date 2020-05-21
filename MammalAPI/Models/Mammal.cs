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

        [MaxLength(70)]
        public string Name { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }

        [MaxLength(100)]
        public string LatinName { get; set; }
        public int Lifespan { get; set; }
        public Family Family { get; set; }
        public ICollection<MammalHabitat> MammalHabitats { get; set; }
    }
}
