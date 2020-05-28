using MammalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MammalAPI.DTO
{
    public class MammalHabitatDTO
    {
        public int Id { get; set; }
        public int MammalId { get; set; }
        public Mammal Mammal { get; set; }
        public int HabitatId { get; set; }
        public Habitat Habitat { get; set; }
    }
}
