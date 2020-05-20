using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MammalAPI.Models
{
    public class Family
    {
        [Key]
        public int FamilyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<Mammal> Mammals { get; set; }
    }
}
