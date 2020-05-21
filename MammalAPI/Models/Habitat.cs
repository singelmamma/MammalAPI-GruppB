using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MammalAPI.Models
{
    public class Habitats
    {
        [Key]
        public int HabitatID { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }

        public IList<MammalHabitat> MammalHabitats { get; set; }
    }
}
