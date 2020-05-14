using System.ComponentModel.DataAnnotations;

namespace MammalAPI.Models
{
    public class Family
    {
        [Key]
        public int FamilyId { get; set; }

        [Required]
        [MaxLength]
        public string Name { get; set; }
    }
}
