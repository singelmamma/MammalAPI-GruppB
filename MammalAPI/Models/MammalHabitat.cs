using System.ComponentModel.DataAnnotations;

namespace MammalAPI.Models
{
    public class MammalHabitat
    {
        [Key]
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public int HabitatId { get; set; }
    }
}
