using System.ComponentModel.DataAnnotations;

namespace MammalAPI.Models
{
    public class MammalHabitat
    {
        [Key]
        public int Id { get; set; }
        public int MammalId { get; set; }
        public Mammal Mammal { get; set; }
        public int HabitatId { get; set; }
        public Habitats Habitat { get; set; }
    }
}
