using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MammalAPI.Models
{
    public class Habitat
    {
        [Key]
        public int HabitatID { get; set; }
        public string Name { get; set; }

    }
}
