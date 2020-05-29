using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MammalAPI.Models
{
    public class Habitat
    {
        [Key]
        public int HabitatID { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<MammalHabitat> MammalHabitats { get; set; }
    }
}
