using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MammalAPI.Models
{
    public class FakeMammal
    {
        [Key]
        public int FakeMammalId { get; set; }
        public string Name { get; set; }
    }
}
